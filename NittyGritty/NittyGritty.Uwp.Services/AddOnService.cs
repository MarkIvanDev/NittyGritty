using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Store;
using NittyGritty.Services.Core;
using Windows.Services.Store;
using Windows.System;

namespace NittyGritty.Uwp.Services
{
    public class AddOnService : ConfigurableService<AddOn>, IAddOnService
    {
        private readonly StoreContext context;

        public AddOnService()
        {
            context = StoreContext.GetDefault();
        }

        #region Is Active

        public async Task<bool> IsActive(string key)
        {
            return await IsAnyActive(Enumerable.Repeat(GetValue(key) as IActiveAddOn, 1));
        }

        public async Task<bool> IsActive(IActiveAddOn addOn)
        {
            return await IsAnyActive(Enumerable.Repeat(addOn, 1));
        }

        public async Task<bool> IsAnyActive(IEnumerable<string> keys)
        {
            return await IsAnyActive(keys.Select(k => GetValue(k) as IActiveAddOn));
        }

        public async Task<bool> IsAnyActive(IEnumerable<IActiveAddOn> addOns)
        {
            var license = await context.GetAppLicenseAsync();
            foreach (var item in addOns)
            {
                if (item is AddOn addOn)
                {
                    var isActive = license.AddOnLicenses.TryGetValue(addOn.Id, out var l) && l.IsActive;
                    if (isActive)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Access Feature

        public async Task<bool> AccessFeature(string key, Func<bool, Task> feature, bool conditionWhenFree)
        {
            return await AccessFeature(Enumerable.Repeat(GetValue(key) as IActiveAddOn, 1), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IEnumerable<string> keys, Func<bool, Task> feature, bool conditionWhenFree)
        {
            return await AccessFeature(keys.Select(k => GetValue(k) as IActiveAddOn), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IActiveAddOn addOn, Func<bool, Task> feature, bool conditionWhenFree)
        {
            return await AccessFeature(Enumerable.Repeat(addOn, 1), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IEnumerable<IActiveAddOn> addOns, Func<bool, Task> feature, bool conditionWhenFree)
        {
            if (addOns is null)
            {
                throw new ArgumentNullException(nameof(addOns));
            }

            var isActive = await IsAnyActive(addOns);
            if (isActive || conditionWhenFree)
            {
                await feature.Invoke(isActive);
                return true;
            }
            return false;
        }

        public async Task<bool> AccessFeature(string key, Action<bool> feature, bool conditionWhenFree)
        {
            return await AccessFeature(Enumerable.Repeat(GetValue(key) as IActiveAddOn, 1), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IEnumerable<string> keys, Action<bool> feature, bool conditionWhenFree)
        {
            return await AccessFeature(keys.Select(k => GetValue(k) as IActiveAddOn), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IActiveAddOn addOn, Action<bool> feature, bool conditionWhenFree)
        {
            return await AccessFeature(Enumerable.Repeat(addOn, 1), feature, conditionWhenFree);
        }

        public async Task<bool> AccessFeature(IEnumerable<IActiveAddOn> addOns, Action<bool> feature, bool conditionWhenFree)
        {
            if (addOns is null)
            {
                throw new ArgumentNullException(nameof(addOns));
            }

            var isActive = await IsAnyActive(addOns);
            if (isActive || conditionWhenFree)
            {
                feature.Invoke(isActive);
                return true;
            }
            return false;
        }

        #endregion
        
        #region Get AddOns

        public async Task<ConsumableAddOn> GetConsumableAddOn(string key)
        {
            var addOns = await GetConsumableAddOns(Enumerable.Repeat(key, 1));
            return addOns.FirstOrDefault();
        }

        public async Task<IList<ConsumableAddOn>> GetConsumableAddOns()
        {
            return await GetConsumableAddOns(Keys);
        }

        public async Task<IList<ConsumableAddOn>> GetConsumableAddOns(IEnumerable<string> keys)
        {
            var consumables = keys.Select(k => GetValue(k) as ConsumableAddOn).Where(a => a != null).ToList();
            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Consumable" }, consumables.Select(c => c.Id));
            if (queryResult.ExtendedError == null)
            {
                foreach (var consumable in consumables)
                {
                    var product = queryResult.Products[consumable.Id];
                    var balanceResult = await context.GetConsumableBalanceRemainingAsync(product.StoreId);
                    consumable.Title = product.Title;
                    consumable.Description = product.Description;
                    consumable.Price = product.Price.FormattedPrice;
                    consumable.CustomData = product.Skus[0].CustomDeveloperData;
                    consumable.Balance = balanceResult.Status == StoreConsumableStatus.Succeeded ? balanceResult.BalanceRemaining : 0;
                }
            }

            return consumables;
        }

        public async Task<DurableAddOn> GetDurableAddOn(string key)
        {
            var addOns = await GetDurableAddOns(Enumerable.Repeat(key, 1));
            return addOns.FirstOrDefault();
        }

        public async Task<IList<DurableAddOn>> GetDurableAddOns()
        {
            return await GetDurableAddOns(Keys);
        }

        public async Task<IList<DurableAddOn>> GetDurableAddOns(IEnumerable<string> keys)
        {
            var durables = keys.Select(k => GetValue(k) as DurableAddOn).Where(a => a != null).ToList();
            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Durable" }, durables.Select(d => d.Id));
            if (queryResult.ExtendedError == null)
            {
                var license = await context.GetAppLicenseAsync();
                foreach (var durable in durables)
                {
                    var product = queryResult.Products[durable.Id];
                    if (!product.Skus[0].IsSubscription)
                    {
                        durable.Title = product.Title;
                        durable.Description = product.Description;
                        durable.Price = product.Price.FormattedPrice;
                        durable.CustomData = product.Skus[0].CustomDeveloperData;
                        durable.IsActive = license.AddOnLicenses.TryGetValue(product.StoreId, out var l) && l.IsActive;
                    }
                }
            }

            return durables;
        }

        public async Task<SubscriptionAddOn> GetSubscriptionAddOn(string key)
        {
            var addOns = await GetSubscriptionAddOns(Enumerable.Repeat(key, 1));
            return addOns.FirstOrDefault();
        }

        public async Task<IList<SubscriptionAddOn>> GetSubscriptionAddOns()
        {
            return await GetSubscriptionAddOns(Keys);
        }

        public async Task<IList<SubscriptionAddOn>> GetSubscriptionAddOns(IEnumerable<string> keys)
        {
            var subscriptions = keys.Select(k => GetValue(k) as SubscriptionAddOn).Where(a => a != null).ToList();
            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Durable" }, subscriptions.Select(s => s.Id));
            if (queryResult.ExtendedError == null)
            {
                var license = await context.GetAppLicenseAsync();
                foreach (var subscription in subscriptions)
                {
                    var product = queryResult.Products[subscription.Id];
                    if (product.Skus[0].IsSubscription)
                    {
                        subscription.Title = product.Title;
                        subscription.Description = product.Description;
                        subscription.Price = product.Price.FormattedPrice;
                        subscription.CustomData = product.Skus[0].CustomDeveloperData;
                        subscription.BillingPeriod = product.Skus[0].SubscriptionInfo.BillingPeriod;
                        subscription.BillingPeriodUnit = (DurationUnit)product.Skus[0].SubscriptionInfo.BillingPeriodUnit;
                        subscription.TrialPeriod = product.Skus[0].SubscriptionInfo.TrialPeriod;
                        subscription.TrialPeriodUnit = (DurationUnit)product.Skus[0].SubscriptionInfo.TrialPeriodUnit;
                        subscription.IsActive = license.AddOnLicenses.TryGetValue(product.StoreId, out var l) && l.IsActive;
                    }
                }
            }

            return subscriptions;
        }

        public async Task<UnmanagedConsumableAddOn> GetUnmanagedConsumableAddOn(string key)
        {
            var addOns = await GetUnmanagedConsumableAddOns(Enumerable.Repeat(key, 1));
            return addOns.FirstOrDefault();
        }

        public async Task<IList<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns()
        {
            return await GetUnmanagedConsumableAddOns(Keys);
        }

        public async Task<IList<UnmanagedConsumableAddOn>> GetUnmanagedConsumableAddOns(IEnumerable<string> keys)
        {
            var consumables = keys.Select(k => GetValue(k) as UnmanagedConsumableAddOn).Where(a => a != null).ToList();
            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "UnmanagedConsumable" }, consumables.Select(u => u.Id));
            if (queryResult.ExtendedError == null)
            {
                foreach (var consumable in consumables)
                {
                    var product = queryResult.Products[consumable.Id];
                    var balanceResult = await context.GetConsumableBalanceRemainingAsync(product.StoreId);
                    consumable.Title = product.Title;
                    consumable.Description = product.Description;
                    consumable.Price = product.Price.FormattedPrice;
                    consumable.CustomData = product.Skus[0].CustomDeveloperData;
                    consumable.Balance = balanceResult.Status == StoreConsumableStatus.Succeeded ? balanceResult.BalanceRemaining : 0;
                }
            }

            return consumables;
        }

        #endregion
        
        public async Task ManageSubscriptions()
        {
            await Launcher.LaunchUriAsync(new Uri("https://account.microsoft.com/services"));
        }

        public async Task Purchase(string key)
        {
            await InternalPurchase(GetValue(key).Id);
        }

        public async Task Purchase(AddOn addOn)
        {
            await InternalPurchase(addOn.Id);
        }

        public async Task<bool> TryPurchase(string key)
        {
            var addOn = GetValue(key);
            return addOn != null && await InternalTryPurchase(addOn.Id);
        }

        public async Task<bool> TryPurchase(AddOn addOn)
        {
            return await InternalTryPurchase(addOn.Id);
        }

        private async Task InternalPurchase(string storeId)
        {
            var purchaseResult = await context.RequestPurchaseAsync(storeId);
            switch (purchaseResult.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    break;
                case StorePurchaseStatus.AlreadyPurchased:
                    throw new Exception($"You already own this add-on.", purchaseResult.ExtendedError);
                case StorePurchaseStatus.NotPurchased:
                    throw new Exception($"The purchase did not complete or may have been cancelled.", purchaseResult.ExtendedError);
                case StorePurchaseStatus.NetworkError:
                case StorePurchaseStatus.ServerError:
                    throw new Exception($"The purchase was unsuccessful due to a server or network error.", purchaseResult.ExtendedError);
                default:
                    throw new Exception("The purchase was unsuccessful due to an unknown error", purchaseResult.ExtendedError);
            }
        }

        private async Task<bool> InternalTryPurchase(string storeId)
        {
            var purchaseResult = await context.RequestPurchaseAsync(storeId);
            switch (purchaseResult.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    return true;
                case StorePurchaseStatus.AlreadyPurchased:
                case StorePurchaseStatus.NotPurchased:
                case StorePurchaseStatus.NetworkError:
                case StorePurchaseStatus.ServerError:
                default:
                    return false;
            }
        }

        public async Task ReportUnmanagedConsumableFulfillment(string key, string trackingId)
        {
            await UpdateConsumableBalance(key, 1, trackingId);
        }

        public async Task UpdateConsumableBalance(string key, uint quantity, string trackingId)
        {
            var result = await context.ReportConsumableFulfillmentAsync(GetValue(key).Id, quantity, Guid.Parse(trackingId));
            switch (result.Status)
            {
                case StoreConsumableStatus.Succeeded:
                    break;
                case StoreConsumableStatus.InsufficentQuantity:
                    throw new Exception($"The fulfillment was unsuccessful because the remaining " +
                        $"balance is insufficient. Remaining balance: {result.BalanceRemaining}", result.ExtendedError);
                case StoreConsumableStatus.NetworkError:
                case StoreConsumableStatus.ServerError:
                    throw new Exception($"The fulfillment was unsuccessful due to a server or network error.", result.ExtendedError);
                default:
                    throw new Exception($"The fulfillment was unsuccessful due to an unknown error.", result.ExtendedError);
            }
        }
    }
}
