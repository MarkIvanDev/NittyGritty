using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public partial class AddOnService
    {
        Task<ReadOnlyCollection<ConsumableAddOn>> PlatformGetConsumableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<DurableAddOn>> PlatformGetDurableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<SubscriptionAddOn>> PlatformGetSubscriptionAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<UnmanagedConsumableAddOn>> PlatformGetUnmanagedConsumableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsActive(IActiveAddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsAnyActive(IEnumerable<IActiveAddOn> addOns)
        {
            throw new NotImplementedException();
        }

        Task PlatformPurchase(AddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformTryPurchase(AddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task PlatformReportUnmanagedConsumableFulfillment(string key, string trackingId)
        {
            throw new NotImplementedException();
        }

        Task PlatformUpdateConsumableBalance(string key, uint quantity, string trackingId)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IActiveAddOn addOn, Func<bool, Task> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IEnumerable<IActiveAddOn> addOn, Func<bool, Task> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IActiveAddOn addOn, Action<bool> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IEnumerable<IActiveAddOn> addOn, Action<bool> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task PlatformManageSubscriptions()
        {
            throw new NotImplementedException();
        }
    }
}
