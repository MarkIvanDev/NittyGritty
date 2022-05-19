using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NittyGritty.Utilities
{
    public static class HttpClientUtilities
    {
        private static readonly object downloadLock;
        private static readonly HttpClient httpClient;

        static HttpClientUtilities()
        {
            downloadLock = new object();
            httpClient = new HttpClient();
        }

        public static async Task Download(Uri onlinePath, string localPath)
        {
            await Download(onlinePath, localPath, CancellationToken.None, null).ConfigureAwait(false);
        }

        public static async Task Download(Uri onlinePath, string localPath, CancellationToken token)
        {
            await Download(onlinePath, localPath, token, null).ConfigureAwait(false);
        }

        public static async Task Download(Uri onlinePath, string localPath, CancellationToken token, IProgress<ProgressInfo> progressCallback)
        {
            using (var response = await httpClient.GetAsync(onlinePath, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var progressInfo = new ProgressInfo();
                    progressInfo.Start();

                    var totalBytes = response.Content.Headers.ContentLength;
                    var name = Path.GetFileName(onlinePath.AbsolutePath);
                    progressInfo.Update(totalBytes, 0, name);
                    progressCallback?.Report(progressInfo);

                    using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var totalBytesRead = 0L;
                        var buffer = new byte[4096];
                        var isMoreToRead = true;
                        
                        using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                        {
                            do
                            {
                                token.ThrowIfCancellationRequested();
                                var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
                                if (bytesRead == 0)
                                {
                                    isMoreToRead = false;
                                    progressInfo.Update(totalBytes, totalBytesRead, name);
                                    progressCallback?.Report(progressInfo);
                                    continue;
                                }

                                await fileStream.WriteAsync(buffer, 0, bytesRead, token).ConfigureAwait(false);

                                totalBytesRead += bytesRead;
                                progressInfo.Update(totalBytes ?? 0, totalBytesRead, name);
                                progressCallback?.Report(progressInfo);
                            }
                            while (isMoreToRead);
                        }
                    }
                    progressInfo.Stop();
                    progressCallback?.Report(progressInfo);
                }
            }
        }

    }
}
