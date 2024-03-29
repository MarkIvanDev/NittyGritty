﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NittyGritty.Platform.Download;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace NittyGritty.Uwp.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly BackgroundDownloader downloader;
        private readonly ConcurrentDictionary<string, DownloadInfo> activeDownloads;

        public DownloadService()
        {
            downloader = new BackgroundDownloader();
            activeDownloads = new ConcurrentDictionary<string, DownloadInfo>();
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        public async Task Initialize()
        {
            try
            {
                var downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();
                var downloadTasks = new List<Task>();
                foreach (var item in downloads)
                {
                    downloadTasks.Add(HandleDownload(item, false));
                }
                await Task.WhenAll(downloadTasks);
            }
            catch (Exception ex)
            {
                DownloadProgressChanged?.Invoke(null,
                    new DownloadProgressChangedEventArgs(null, DownloadStatus.Error) { ErrorMessage = ex.Message });
            }
        }

        public async Task Download(Uri uri)
        {
            try
            {
                var file = await DownloadsFolder.CreateFileAsync(Path.GetFileName(uri.LocalPath), CreationCollisionOption.ReplaceExisting);
                await Download(uri, new NGFile(file));
            }
            catch (Exception ex)
            {
                DownloadProgressChanged?.Invoke(uri.AbsoluteUri,
                    new DownloadProgressChangedEventArgs(uri.AbsoluteUri, DownloadStatus.Error) { ErrorMessage = ex.Message });
            }
        }

        public async Task Download(Uri uri, IFile file)
        {
            try
            {
                if (activeDownloads.ContainsKey(uri.AbsoluteUri)) return;
                var download = downloader.CreateDownload(uri, file.Context as IStorageFile);
                await HandleDownload(download, true);
            }
            catch (Exception ex)
            {
                DownloadProgressChanged?.Invoke(uri.AbsoluteUri,
                    new DownloadProgressChangedEventArgs(uri.AbsoluteUri, DownloadStatus.Error) { ErrorMessage = ex.Message });
            }
        }

        public async Task HandleDownload(DownloadOperation download, bool start)
        {
            var downloadPath = download.RequestedUri.AbsoluteUri;
            try
            {
                DownloadProgressChanged?.Invoke(downloadPath, new DownloadProgressChangedEventArgs(downloadPath));

                // Don't download if there is a current download operation for the URI
                var token = new CancellationTokenSource();
                if (activeDownloads.TryAdd(downloadPath, new DownloadInfo(download, token)))
                {
                    _ = start ?
                        await download.StartAsync().AsTask(token.Token, new Progress<DownloadOperation>(DownloadProgress)) :
                        await download.AttachAsync().AsTask(token.Token, new Progress<DownloadOperation>(DownloadProgress));
                }

                DownloadProgressChanged?.Invoke(downloadPath, new DownloadProgressChangedEventArgs(downloadPath, DownloadStatus.Completed));
            }
            catch (Exception ex)
            {
                DownloadProgressChanged?.Invoke(downloadPath,
                    new DownloadProgressChangedEventArgs(downloadPath, DownloadStatus.Error) { ErrorMessage = ex.Message });
            }
            finally
            {
                activeDownloads.TryRemove(downloadPath, out _);
            }
        }

        void DownloadProgress(DownloadOperation operation)
        {
            var downloadPath = operation.RequestedUri.AbsoluteUri;
            var currentProgress = operation.Progress;
            var currentError = operation.CurrentWebErrorStatus;

            var currentStatus = DownloadStatus.Unknown;
            switch (currentProgress.Status)
            {
                case BackgroundTransferStatus.Idle:
                    currentStatus = DownloadStatus.Idle;
                    break;

                case BackgroundTransferStatus.Running:
                    currentStatus = DownloadStatus.Running;
                    break;

                case BackgroundTransferStatus.PausedByApplication:
                case BackgroundTransferStatus.PausedCostedNetwork:
                case BackgroundTransferStatus.PausedNoNetwork:
                case BackgroundTransferStatus.PausedRecoverableWebErrorStatus:
                case BackgroundTransferStatus.PausedSystemPolicy:
                    currentStatus = DownloadStatus.Paused;
                    break;

                case BackgroundTransferStatus.Completed:
                    currentStatus = DownloadStatus.Completed;
                    break;

                case BackgroundTransferStatus.Canceled:
                    currentStatus = DownloadStatus.Canceled;
                    break;

                case BackgroundTransferStatus.Error:
                    currentStatus = DownloadStatus.Error;
                    break;
            }

            DownloadProgressChanged?.Invoke(downloadPath,
                new DownloadProgressChangedEventArgs(downloadPath, currentStatus,
                    currentProgress.TotalBytesToReceive == 0 ? (double?)null : currentProgress.TotalBytesToReceive, currentProgress.BytesReceived)
                { ErrorMessage = currentError?.ToString() });
        }

        public void Pause(Uri uri)
        {
            if (activeDownloads.TryGetValue(uri.AbsoluteUri, out var info))
            {
                var progress = info.Operation.Progress;
                if (progress.Status == BackgroundTransferStatus.Running)
                {
                    info.Operation.Pause();
                }
            }
        }

        public void Resume(Uri uri)
        {
            if (activeDownloads.TryGetValue(uri.AbsoluteUri, out var info))
            {
                var progress = info.Operation.Progress;
                if (progress.Status == BackgroundTransferStatus.PausedByApplication)
                {
                    info.Operation.Resume();
                }
            }
        }

        public void Cancel(Uri uri)
        {
            if (activeDownloads.TryGetValue(uri.AbsoluteUri, out var info))
            {
                info.Token.Cancel();
                info.Token.Dispose();
            }
        }

        private class DownloadInfo
        {
            public DownloadInfo(DownloadOperation operation, CancellationTokenSource token)
            {
                Operation = operation;
                Token = token;
            }

            public DownloadOperation Operation { get; }

            public CancellationTokenSource Token { get; }
        }
    }
}
