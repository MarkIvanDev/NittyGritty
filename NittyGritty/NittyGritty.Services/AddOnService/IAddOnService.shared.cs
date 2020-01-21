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

        // Durable
        Task<DurableAddOn> GetDurableAddOn(string key);
        Task<ReadOnlyCollection<DurableAddOn>> GetDurableAddOns(params string[] keys);
        Task<bool> IsDurableActive(string key);

        // Consumable
        Task<ConsumableAddOn> GetConsumableAddOn(string key);
        Task<ReadOnlyCollection<ConsumableAddOn>> GetConsumableAddOns(params string[] keys);
        Task<string> UpdateConsumableBalance(string key, uint quantity);

        // UnmanagedConsumable
        Task<UnmanagedConsumableAddOn> GetUnmanagedConsumableAddOn(string key);
        Task<ReadOnlyCollection<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns(params string[] keys);
        Task<string> ReportUnmanagedConsumableFulfillment(string key);

        // Subscription
        Task<SubscriptionAddOn> GetSubscriptionAddOn(string key);
        Task<ReadOnlyCollection<SubscriptionAddOn>> GetSubscriptionAddOns(params string[] keys);
        Task<bool> IsSubscriptionActive(string key);

    }
}
