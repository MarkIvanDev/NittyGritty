using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Store;
using Windows.Services.Store;

namespace NittyGritty.Services
{
    public partial class AddOnService
    {
        private StoreContext context = null;

        async Task<ConsumableAddOn> PlatformGetConsumableAddOn(string key)
        {
            var addOns = await PlatformGetConsumableAddOns(key);
            return addOns.FirstOrDefault();
        }

        async Task<ReadOnlyCollection<ConsumableAddOn>> PlatformGetConsumableAddOns(params string[] keys)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
            var consumables = new Collection<ConsumableAddOn>();

            // Specify the kinds of add-ons to retrieve.
            var storeIds = new Collection<string>();
            if (keys.Length == 0)
            {
                foreach (var item in _addOnsByKey.Values.OfType<ConsumableAddOn>())
                {
                    storeIds.Add(item.Id);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    storeIds.Add(_addOnsByKey[key].Id);
                }
            }
            
            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Consumable" }, storeIds);
            if (queryResult.ExtendedError == null)
            {
                var products = queryResult.Products.OrderBy(p => p.Value.Title);
                foreach (var item in products)
                {
                    // Access the Store info for the product.
                    StoreProduct product = item.Value;

                    // Use members of the product object to access info for the product...
                    var balanceResult = await context.GetConsumableBalanceRemainingAsync(product.StoreId);
                    consumables.Add(new ConsumableAddOn(product.StoreId)
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price.FormattedPrice,
                        CustomData = product.Skus[0].CustomDeveloperData,
                        Balance = balanceResult.Status == StoreConsumableStatus.Succeeded ? balanceResult.BalanceRemaining : 0,
                        // TODO: Read quantity in ExtendedJsonData
                    });
                }
            }

            return new ReadOnlyCollection<ConsumableAddOn>(consumables);
        }

        async Task<DurableAddOn> PlatformGetDurableAddOn(string key)
        {
            var addOns = await PlatformGetDurableAddOns(key);
            return addOns.FirstOrDefault();
        }

        async Task<ReadOnlyCollection<DurableAddOn>> PlatformGetDurableAddOns(params string[] keys)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
            var durables = new Collection<DurableAddOn>();

            // Specify the kinds of add-ons to retrieve.
            var storeIds = new Collection<string>();
            if (keys.Length == 0)
            {
                foreach (var item in _addOnsByKey.Values.OfType<DurableAddOn>())
                {
                    storeIds.Add(item.Id);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    storeIds.Add(_addOnsByKey[key].Id);
                }
            }

            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Durable" }, storeIds);
            if (queryResult.ExtendedError == null)
            {
                var products = queryResult.Products.OrderBy(p => p.Value.Title);
                foreach (var item in products)
                {
                    // Access the Store info for the product.
                    StoreProduct product = item.Value;

                    if(!product.Skus[0].IsSubscription)
                    {
                        // Use members of the product object to access info for the product...
                        durables.Add(new DurableAddOn(product.StoreId)
                        {
                            Title = product.Title,
                            Description = product.Description,
                            Price = product.Price.FormattedPrice,
                            CustomData = product.Skus[0].CustomDeveloperData,
                            // TODO: Read lifetime from ExtendedJsonData
                        });
                    }

                    
                }
            }

            return new ReadOnlyCollection<DurableAddOn>(durables);
        }

        async Task<SubscriptionAddOn> PlatformGetSubscriptionAddOn(string key)
        {
            var addOns = await PlatformGetSubscriptionAddOns(key);
            return addOns.FirstOrDefault();
        }

        async Task<ReadOnlyCollection<SubscriptionAddOn>> PlatformGetSubscriptionAddOns(params string[] keys)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
            var subscriptions = new Collection<SubscriptionAddOn>();

            // Specify the kinds of add-ons to retrieve.
            var storeIds = new Collection<string>();
            if (keys.Length == 0)
            {
                foreach (var item in _addOnsByKey.Values.OfType<SubscriptionAddOn>())
                {
                    storeIds.Add(item.Id);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    storeIds.Add(_addOnsByKey[key].Id);
                }
            }

            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "Durable" }, storeIds);
            if (queryResult.ExtendedError == null)
            {
                var products = queryResult.Products.OrderBy(p => p.Value.Title);
                foreach (var item in products)
                {
                    // Access the Store info for the product.
                    StoreProduct product = item.Value;

                    if (product.Skus[0].IsSubscription)
                    {
                        // Use members of the product object to access info for the product...
                        subscriptions.Add(new SubscriptionAddOn(product.StoreId)
                        {
                            Title = product.Title,
                            Description = product.Description,
                            Price = product.Price.FormattedPrice,
                            CustomData = product.Skus[0].CustomDeveloperData,
                            BillingPeriod = product.Skus[0].SubscriptionInfo.BillingPeriod,
                            BillingPeriodUnit = (DurationUnit)product.Skus[0].SubscriptionInfo.BillingPeriodUnit,
                            TrialPeriod = product.Skus[0].SubscriptionInfo.TrialPeriod,
                            TrialPeriodUnit = (DurationUnit)product.Skus[0].SubscriptionInfo.TrialPeriodUnit
                        });
                    }
                }
            }

            return new ReadOnlyCollection<SubscriptionAddOn>(subscriptions);
        }

        async Task<UnmanagedConsumableAddOn> PlatformGetUnmanagedConsumableAddOn(string key)
        {
            var addOns = await PlatformGetUnmanagedConsumableAddOns(key);
            return addOns.FirstOrDefault();
        }

        async Task<ReadOnlyCollection<UnmanagedConsumableAddOn>> PlatformGetUnmanagedConsumableAddOns(params string[] keys)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
            var consumables = new Collection<UnmanagedConsumableAddOn>();

            // Specify the kinds of add-ons to retrieve.
            var storeIds = new Collection<string>();
            if (keys.Length == 0)
            {
                foreach (var item in _addOnsByKey.Values.OfType<UnmanagedConsumableAddOn>())
                {
                    storeIds.Add(item.Id);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    storeIds.Add(_addOnsByKey[key].Id);
                }
            }

            var queryResult = await context.GetStoreProductsAsync(new List<string>() { "UnmanagedConsumable" }, storeIds);
            if (queryResult.ExtendedError == null)
            {
                var products = queryResult.Products.OrderBy(p => p.Value.Title);
                foreach (var item in products)
                {
                    // Access the Store info for the product.
                    StoreProduct product = item.Value;

                    // Use members of the product object to access info for the product...
                    var balanceResult = await context.GetConsumableBalanceRemainingAsync(product.StoreId);
                    consumables.Add(new UnmanagedConsumableAddOn(product.StoreId)
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price.FormattedPrice,
                        CustomData = product.Skus[0].CustomDeveloperData,
                        Balance = balanceResult.Status == StoreConsumableStatus.Succeeded ? balanceResult.BalanceRemaining : 0
                    });
                }
            }

            return new ReadOnlyCollection<UnmanagedConsumableAddOn>(consumables);
        }

        async Task<bool> PlatformIsDurableActive(string key)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }

            // Specify the kinds of add-ons to retrieve.
            var license = await context.GetAppLicenseAsync();
            return license.AddOnLicenses.TryGetValue(_addOnsByKey[key].Id, out var l);
        }

        async Task<bool> PlatformIsSubscriptionActive(string key)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }

            // Specify the kinds of add-ons to retrieve.
            var license = await context.GetAppLicenseAsync();
            return license.AddOnLicenses.TryGetValue(_addOnsByKey[key].Id, out var l);
        }

        async Task PlatformPurchase(string key)
        {
            await PlatformPurchaseById(_addOnsByKey[key].Id);
        }

        async Task PlatformPurchase(AddOn addOn)
        {
            await PlatformPurchaseById(addOn.Id);
        }

        async Task PlatformPurchaseById(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }

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
            }
        }

        async Task<string> PlatformReportUnmanagedConsumableFulfillment(string key)
        {
            return await PlatformUpdateConsumableBalance(key, 1);
        }

        async Task<string> PlatformUpdateConsumableBalance(string key, uint quantity)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
            var trackingId = Guid.NewGuid();
            var result = await context.ReportConsumableFulfillmentAsync(_addOnsByKey[key].Id, quantity, trackingId);
            switch (result.Status)
            {
                case StoreConsumableStatus.Succeeded:
                    return result.TrackingId.ToString();
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
