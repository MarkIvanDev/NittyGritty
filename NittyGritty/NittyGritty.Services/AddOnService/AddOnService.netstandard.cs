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
        Task<ConsumableAddOn> PlatformGetConsumableAddOn(string key)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<ConsumableAddOn>> PlatformGetConsumableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<DurableAddOn> PlatformGetDurableAddOn(string key)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<DurableAddOn>> PlatformGetDurableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<SubscriptionAddOn> PlatformGetSubscriptionAddOn(string key)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<SubscriptionAddOn>> PlatformGetSubscriptionAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<UnmanagedConsumableAddOn> PlatformGetUnmanagedConsumableAddOn(string key)
        {
            throw new NotImplementedException();
        }

        Task<ReadOnlyCollection<UnmanagedConsumableAddOn>> PlatformGetUnmanagedConsumableAddOns(params string[] keys)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsDurableActive(string key)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsDurableActive(DurableAddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsSubscriptionActive(string key)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsSubscriptionActive(SubscriptionAddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformIsActive(IActiveAddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task PlatformPurchase(string key)
        {
            throw new NotImplementedException();
        }

        Task PlatformPurchase(AddOn addOn)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformTryPurchase(string key)
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

        Task<bool> PlatformAccessFeature(string key, Func<bool, Task> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IActiveAddOn addOn, Func<bool, Task> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(string key, Action<bool> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformAccessFeature(IActiveAddOn addOn, Action<bool> feature, bool conditionWhenFree)
        {
            throw new NotImplementedException();
        }
    }
}
