using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

        public static async Task Download(Uri onlinePath, string localPath, IProgress<ProgressInfo> progressCallback = null)
        {
            using (var response = await httpClient.GetAsync(onlinePath, HttpCompletionOption.ResponseContentRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var totalBytes = response.Content.Headers.ContentLength;
                    var name = Path.GetFileName(onlinePath.AbsolutePath);
                    var progressInfo = new ProgressInfo();
                    progressInfo.Update(totalBytes, 0, name);
                    progressCallback?.Report(progressInfo);

                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        var totalBytesRead = 0L;
                        var buffer = new byte[4096];
                        var isMoreToRead = true;
                        
                        using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                        {
                            do
                            {
                                var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                                if (bytesRead == 0)
                                {
                                    isMoreToRead = false;
                                    progressInfo.Update(totalBytes, totalBytesRead, name);
                                    progressCallback?.Report(progressInfo);
                                    continue;
                                }

                                await fileStream.WriteAsync(buffer, 0, bytesRead);

                                totalBytesRead += bytesRead;
                                progressInfo.Update(totalBytes, totalBytesRead, name);
                                progressCallback?.Report(progressInfo);
                            }
                            while (isMoreToRead);
                        }
                    }  
                }
            }
        }

    }
}
