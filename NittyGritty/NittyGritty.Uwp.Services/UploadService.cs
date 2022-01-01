using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using NittyGritty.Platform.Upload;
using NittyGritty.Services.Core;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace NittyGritty.Uwp.Services
{
    public class UploadService : IUploadService
    {
        private readonly BackgroundUploader uploader;
        private readonly ConcurrentDictionary<string, UploadInfo> activeUploads;

        public UploadService()
        {
            uploader = new BackgroundUploader();
            activeUploads = new ConcurrentDictionary<string, UploadInfo>();
        }

        public event UploadProgressChangedEventHandler UploadProgressChanged;

        public async Task Initialize()
        {
            try
            {
                var uploads = await BackgroundUploader.GetCurrentUploadsAsync();
                var uploadTasks = new List<Task>();
                foreach (var item in uploads)
                {
                    uploadTasks.Add(PlatformHandleUpload(item, false));
                }
                await Task.WhenAll(uploadTasks);
            }
            catch (Exception)
            {

            }
        }

        public async Task Upload(Uri uri, IFile file)
        {
            try
            {
                if (activeUploads.ContainsKey(uri.AbsoluteUri)) return;

                var upload = uploader.CreateUpload(uri, file.Context as IStorageFile);
                await PlatformHandleUpload(upload, true);
            }
            catch (Exception ex)
            {
                UploadProgressChanged?.Invoke(uri.AbsoluteUri,
                    new UploadProgressChangedEventArgs(uri.AbsoluteUri, UploadStatus.Error) { ErrorMessage = ex.Message });
            }
        }

        public async Task Upload(Uri uri, IList<IFile> files)
        {
            try
            {
                if (activeUploads.ContainsKey(uri.AbsoluteUri)) return;

                var parts = new List<BackgroundTransferContentPart>();
                for (int i = 0; i < files.Count; i++)
                {
                    var part = new BackgroundTransferContentPart($"File {i}", files[i].Name);
                    part.SetFile(files[i].Context as IStorageFile);
                    parts.Add(part);
                }

                var upload = await uploader.CreateUploadAsync(uri, parts);
                await PlatformHandleUpload(upload, true);
            }
            catch (Exception ex)
            {
                UploadProgressChanged?.Invoke(uri.AbsoluteUri,
                    new UploadProgressChangedEventArgs(uri.AbsoluteUri, UploadStatus.Error) { ErrorMessage = ex.Message });
            }
        }

        async Task PlatformHandleUpload(UploadOperation upload, bool start)
        {
            var uploadPath = upload.RequestedUri.AbsoluteUri;
            try
            {
                UploadProgressChanged?.Invoke(uploadPath, new UploadProgressChangedEventArgs(uploadPath));

                // Don't upload if there is a current upload operation for the URI
                var token = new CancellationTokenSource();
                if (activeUploads.TryAdd(uploadPath, new UploadInfo(upload, token)))
                {
                    _ = start ?
                        await upload.StartAsync().AsTask(token.Token, new Progress<UploadOperation>(UploadProgress)) :
                        await upload.AttachAsync().AsTask(token.Token, new Progress<UploadOperation>(UploadProgress));
                }

                UploadProgressChanged?.Invoke(uploadPath, new UploadProgressChangedEventArgs(uploadPath, UploadStatus.Completed));
            }
            catch (Exception ex)
            {
                UploadProgressChanged?.Invoke(uploadPath,
                    new UploadProgressChangedEventArgs(uploadPath, UploadStatus.Error) { ErrorMessage = ex.Message });
            }
            finally
            {
                activeUploads.TryRemove(uploadPath, out _);
            }
        }

        void UploadProgress(UploadOperation operation)
        {
            var uploadPath = operation.RequestedUri.AbsoluteUri;
            var currentProgress = operation.Progress;

            var currentStatus = UploadStatus.Unknown;
            switch (currentProgress.Status)
            {
                case BackgroundTransferStatus.Idle:
                    currentStatus = UploadStatus.Idle;
                    break;

                case BackgroundTransferStatus.Running:
                    currentStatus = UploadStatus.Running;
                    break;

                case BackgroundTransferStatus.PausedByApplication:
                case BackgroundTransferStatus.PausedCostedNetwork:
                case BackgroundTransferStatus.PausedNoNetwork:
                case BackgroundTransferStatus.PausedRecoverableWebErrorStatus:
                case BackgroundTransferStatus.PausedSystemPolicy:
                    currentStatus = UploadStatus.Paused;
                    break;

                case BackgroundTransferStatus.Completed:
                    currentStatus = UploadStatus.Completed;
                    break;

                case BackgroundTransferStatus.Canceled:
                    currentStatus = UploadStatus.Canceled;
                    break;

                case BackgroundTransferStatus.Error:
                    currentStatus = UploadStatus.Error;
                    break;
            }

            UploadProgressChanged?.Invoke(uploadPath,
                new UploadProgressChangedEventArgs(uploadPath, currentStatus,
                    currentProgress.TotalBytesToSend == 0 ? (double?)null : currentProgress.TotalBytesToSend, currentProgress.BytesSent));
        }

        public void Cancel(Uri uri)
        {
            if (activeUploads.TryGetValue(uri.AbsoluteUri, out var info))
            {
                info.Token.Cancel();
                info.Token.Dispose();
            }
        }

        private class UploadInfo
        {
            public UploadInfo(UploadOperation operation, CancellationTokenSource token)
            {
                Operation = operation;
                Token = token;
            }

            public UploadOperation Operation { get; }

            public CancellationTokenSource Token { get; }
        }
    }
}
