using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public partial class StoreService : IStoreService
    {

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
