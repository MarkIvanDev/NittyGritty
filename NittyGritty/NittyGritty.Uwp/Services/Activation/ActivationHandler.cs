using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    public interface IActivationHandler
    {
        Frame NavigationContext { get; }

        ActivationStrategy Strategy { get; }

        bool CanHandle(object args);

        Task Handle(object args);

        void SetNavigationContext(Frame frame);
    }

    public abstract class ActivationHandler<T> : IActivationHandler
        where T : class
    {
        public ActivationHandler(ActivationStrategy strategy)
        {
            if(strategy == ActivationStrategy.Unknown)
            {
                throw new ArgumentException("Strategy cannot be Unknown", nameof(strategy));
            }

            Strategy = strategy;
        }

        public ActivationStrategy Strategy { get; }

        public Frame NavigationContext { get; private set; }

        public void SetNavigationContext(Frame frame)
        {
            NavigationContext = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        public bool CanHandle(object args)
        {
            return args is T t && CanHandleInternal(t);
        }

        public async Task Handle(object args)
        {
            if(args is T t)
            {
                await HandleInternal(t);
            }
        }

        protected virtual async Task HandleInternal(T args)
        {
            await Task.CompletedTask;
        }

        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }

    }
    
    public enum ActivationStrategy
    {
        Unknown = 0,
        Normal = 1,
        Background = 2,
        Hosted = 3
    }
}
