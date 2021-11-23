using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class StoreService : IStoreService
    {
        public StoreService(string appId)
        {
            AppId = appId;
        }

        public string AppId { get; }

        public Task RequestRating()
            => PlatformRequestRating();

        public Task<bool> CheckForUpdates()
            => PlatformCheckForUpdates();

        public Task<bool> CheckForMandatoryUpdates()
            => PlatformCheckForMandatoryUpdates();

        public Task DownloadUpdates()
            => PlatformDownloadUpdates();

        public Task DownloadAndInstallUpdates()
            => PlatformDownloadAndInstallUpdates();

    }
}
