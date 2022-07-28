using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NittyGritty.DependencyInjection
{
    public class Ioc : IDisposable, IAsyncDisposable
    {
        #region Static Members

        private static readonly Lazy<Ioc> lazyDefault;
        private static readonly ConcurrentDictionary<string, Ioc> container;

        static Ioc()
        {
            lazyDefault = new Lazy<Ioc>(() => new Ioc());
            container = new ConcurrentDictionary<string, Ioc>();
        }

        public static Ioc Default => lazyDefault.Value;

        public static Ioc GetOrAdd(string key)
        {
            return container.GetOrAdd(key, new Ioc());
        }

        public static void Remove(string key)
        {
            container.TryRemove(key, out _);
        }

        #endregion

        private readonly ServiceCollection services;
        private readonly Lazy<ServiceProvider> provider;

        public Ioc()
        {
            services = new ServiceCollection();
            provider = new Lazy<ServiceProvider>(() => services.BuildServiceProvider());
        }

        public void Configure(Action<IServiceCollection> services)
        {
            services(this.services);
        }

        #region Provider
        public AsyncServiceScope CreateAsyncScope()
        {
            return provider.Value.CreateAsyncScope();
        }

        public IServiceScope CreateScope()
        {
            return provider.Value.CreateScope();
        }

        public object GetRequiredService(Type serviceType)
        {
            return provider.Value.GetRequiredService(serviceType);
        }

        public T GetRequiredService<T>()
        {
            return provider.Value.GetRequiredService<T>();
        }

        public object GetService(Type serviceType)
        {
            return provider.Value.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return provider.Value.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return provider.Value.GetServices<T>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return provider.Value.GetServices(serviceType);
        }

        public object CreateInstance(Type instanceType, params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance(provider.Value, instanceType, parameters);
        }

        public T CreateInstance<T>(params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance<T>(provider.Value, parameters);
        }

        public T GetServiceOrCreateInstance<T>()
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<T>(provider.Value);
        }

        public object GetServiceOrCreateInstance(Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider.Value, type);
        }

        public T GetInstance<T>(Func<ServiceProvider, T> instanceFactory)
        {
            return instanceFactory(provider.Value);
        }

        #endregion

        public async ValueTask DisposeAsync()
        {
            await provider.Value.DisposeAsync();
        }

        public void Dispose()
        {
            provider.Value.Dispose();
        }
    }
}
