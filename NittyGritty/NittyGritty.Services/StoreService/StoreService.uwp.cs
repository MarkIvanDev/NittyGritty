using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;

namespace NittyGritty.Services
{
    public partial class StoreService
    {
        private StoreContext context = null;

        async Task<bool> PlatformCheckForUpdates()
        {
            context = context ?? StoreContext.GetDefault();

            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            return updates.Count > 0;
        }

        async Task<bool> PlatformCheckForMandatoryUpdates()
        {
            context = context ?? StoreContext.GetDefault();

            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            return updates.FirstOrDefault(u => u.Mandatory) != null;
        }

        async Task PlatformDownloadUpdates()
        {
            context = context ?? StoreContext.GetDefault();

            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();

            if (updates.Count > 0)
            {
                await context.RequestDownloadStorePackageUpdatesAsync(updates);
            }
        }

        async Task PlatformDownloadAndInstallUpdates()
        {
            context = context ?? StoreContext.GetDefault();

            var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();

            if (updates.Count > 0)
            {
                await context.RequestDownloadAndInstallStorePackageUpdatesAsync(updates);
            }
        }
    }
}
