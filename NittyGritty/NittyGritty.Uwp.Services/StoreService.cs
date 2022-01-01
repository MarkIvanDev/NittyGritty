using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Services.Core;
using Windows.Services.Store;
using Windows.System;

namespace NittyGritty.Uwp.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoreContext context;

        public StoreService()
        {
            context = StoreContext.GetDefault();
        }

        public async Task RequestRating()
        {
            _ = await context.RequestRateAndReviewAppAsync();
        }

        public async Task<bool> CheckForUpdates()
        {
            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            return updates.Count > 0;
        }

        public async Task<bool> CheckForMandatoryUpdates()
        {
            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            return updates.FirstOrDefault(u => u.Mandatory) != null;
        }

        public async Task DownloadUpdates()
        {
            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();

            if (updates.Count > 0)
            {
                await context.RequestDownloadStorePackageUpdatesAsync(updates);
            }
        }

        public async Task DownloadAndInstallUpdates()
        {
            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();

            if (updates.Count > 0)
            {
                await context.RequestDownloadAndInstallStorePackageUpdatesAsync(updates);
            }
        }

    }
}
