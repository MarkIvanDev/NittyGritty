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
        bool NeedsNavigationContext { get; }

        Frame NavigationContext { get; }

        ActivationStrategy Strategy { get; }

        bool CanHandle(object args);

        Task HandleAsync(object args);

        void SetNavigationContext(Frame frame);
    }

    public class ActivationHandler<T> : IActivationHandler
        where T : class
    {
        public ActivationStrategy Strategy { get; protected set; } = ActivationStrategy.Normal;

        public bool NeedsNavigationContext { get; protected set; }

        public Frame NavigationContext { get; protected set; }

        public virtual async Task HandleAsync(T args)
        {
            await Task.FromResult(false);
        }

        public virtual bool CanHandle(T args)
        {
            return args != null;
        }

        public void SetNavigationContext(Frame frame)
        {
            NavigationContext = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        bool IActivationHandler.CanHandle(object args)
        {
            return CanHandle(args as T);
        }

        async Task IActivationHandler.HandleAsync(object args)
        {
            await HandleAsync(args as T);
        }

    }

    public enum ActivationStrategy
    {
        Normal = 0,
        Background = 1,
        Picker = 2,
        Other = 9
    }
}
