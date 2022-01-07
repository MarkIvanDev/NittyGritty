using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services.Core
{
    public interface IAddOnService
    {
        // Common
        Task Purchase(string key);
        Task Purchase(AddOn addOn);
        Task<bool> TryPurchase(string key);
        Task<bool> TryPurchase(AddOn addOn);
        Task<bool> IsActive(string key);
        Task<bool> IsActive(IActiveAddOn addOn);
        Task<bool> IsAnyActive(IEnumerable<string> keys);
        Task<bool> IsAnyActive(IEnumerable<IActiveAddOn> addOns);

        // Durable
        Task<DurableAddOn> GetDurableAddOn(string key);
        Task<IList<DurableAddOn>> GetDurableAddOns();
        Task<IList<DurableAddOn>> GetDurableAddOns(IEnumerable<string> keys);

        // Consumable
        Task<ConsumableAddOn> GetConsumableAddOn(string key);
        Task<IList<ConsumableAddOn>> GetConsumableAddOns();
        Task<IList<ConsumableAddOn>> GetConsumableAddOns(IEnumerable<string> keys);
        Task UpdateConsumableBalance(string key, uint quantity, string trackingId);

        // UnmanagedConsumable
        Task<UnmanagedConsumableAddOn> GetUnmanagedConsumableAddOn(string key);
        Task<IList<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns();
        Task<IList<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns(IEnumerable<string> keys);
        Task ReportUnmanagedConsumableFulfillment(string key, string trackingId);

        // Subscription
        Task<SubscriptionAddOn> GetSubscriptionAddOn(string key);
        Task<IList<SubscriptionAddOn>> GetSubscriptionAddOns();
        Task<IList<SubscriptionAddOn>> GetSubscriptionAddOns(IEnumerable<string> keys);

        // Helpers
        Task<bool> AccessFeature(string key, Func<bool, Task> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IEnumerable<string> keys, Func<bool, Task> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IActiveAddOn addOn, Func<bool, Task> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IEnumerable<IActiveAddOn> addOns, Func<bool, Task> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(string key, Action<bool> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IEnumerable<string> keys, Action<bool> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IActiveAddOn addOn, Action<bool> feature, bool conditionWhenFree);
        Task<bool> AccessFeature(IEnumerable<IActiveAddOn> addOns, Action<bool> feature, bool conditionWhenFree);
        Task ManageSubscriptions();
    }
}
