using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public interface IAddOnService
    {
        // Common
        Task Purchase(string key);
        Task Purchase(AddOn addOn);
        Task<bool> TryPurchase(string key);
        Task<bool> TryPurchase(AddOn addOn);
        Task<bool> IsActive(IActiveAddOn addOn);

        // Durable
        Task<DurableAddOn> GetDurableAddOn(string key);
        Task<ReadOnlyCollection<DurableAddOn>> GetDurableAddOns(params string[] keys);
        Task<bool> IsDurableActive(string key);
        Task<bool> IsDurableActive(DurableAddOn addOn);

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
    }
}
