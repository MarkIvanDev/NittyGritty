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
        Task<ReadOnlyCollection<DurableAddOn>> GetDurableAddOns(params string[] keys);
        Task<bool> IsDurableActive(string key);
        Task<bool> IsDurableActive(DurableAddOn addOn);
        Task<bool> IsAnyDurableActive(IEnumerable<string> keys);
        Task<bool> IsAnyDurableActive(IEnumerable<DurableAddOn> addOns);

        // Consumable
        Task<ConsumableAddOn> GetConsumableAddOn(string key);
        Task<ReadOnlyCollection<ConsumableAddOn>> GetConsumableAddOns(params string[] keys);
        Task UpdateConsumableBalance(string key, uint quantity, string trackingId);

        // UnmanagedConsumable
        Task<UnmanagedConsumableAddOn> GetUnmanagedConsumableAddOn(string key);
        Task<ReadOnlyCollection<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns(params string[] keys);
        Task ReportUnmanagedConsumableFulfillment(string key, string trackingId);

        // Subscription
        Task<SubscriptionAddOn> GetSubscriptionAddOn(string key);
        Task<ReadOnlyCollection<SubscriptionAddOn>> GetSubscriptionAddOns(params string[] keys);
        Task<bool> IsSubscriptionActive(string key);
        Task<bool> IsSubscriptionActive(SubscriptionAddOn addOn);
        Task<bool> IsAnySubscriptionActive(IEnumerable<string> keys);
        Task<bool> IsAnySubscriptionActive(IEnumerable<SubscriptionAddOn> addOns);

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
