using NittyGritty.Uwp.Services.Activation;
using NittyGritty.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services
{
    public class ActivationService
    {
        private readonly Func<Task> initialization;
        private readonly Lazy<UIElement> shell;
        private readonly Lazy<Frame> navigationContext;
        private readonly IEnumerable<IActivationHandler> handlers;
        private readonly DefaultActivationHandler defaultHandler;
        private readonly Func<Task> startup;

        public ActivationService(Func<Task> initialization,
                                 Lazy<UIElement> shell,
                                 Lazy<Frame> navigationContext,
                                 IEnumerable<IActivationHandler> handlers,
                                 Type defaultView,
                                 Func<Task> startup)
        {
            this.initialization = initialization;
            this.shell = shell;
            this.navigationContext = navigationContext;
            this.handlers = handlers;
            this.defaultHandler = defaultView != null ? new DefaultActivationHandler(defaultView) : throw new ArgumentNullException(nameof(defaultView), "Default activation handler can not be null");
            this.startup = startup;
        }

        public ActivationService(INittyGrittyApp app) : this(
            app.Initialization,
            new Lazy<UIElement>(app.CreateShell),
            new Lazy<Frame>(app.GetNavigationContext),
            app.GetActivationHandlers(),
            app.DefaultView,
            app.Startup)
        {
        }

        public async Task ActivateAsync(object args)
        {
            var activationHandler = handlers?.FirstOrDefault(h => h.CanHandle(args));

            // Default activation and ALL Normal activations (exclude Background, Hosted and Custom activations)
            if (activationHandler == null || activationHandler.Strategy == ActivationStrategy.Normal)
            {
                // Initialize things like registering background task before the app is loaded
                await initialization?.Invoke();

                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Frame to act as the navigation context and navigate to the first page
                    Window.Current.Content = shell?.Value ?? new Frame();
                }
            }
            // Share Target, File Open Picker, File Save Picker, Contact Panel activation
            // Hosted handlers must be assured that they can use the current Window's content as a Frame for their navigation context
            else if(activationHandler.Strategy == ActivationStrategy.Hosted)
            {
                Window.Current.Content = new Frame();
            }
            // Background activations do not need UI so we are not going to initialize the current Window's content
            // Other activations must handle their own UI logic in their implementations (HandleAsync)
            
            if (activationHandler != null)
            {
                // Normal activations can request for a navigation context (in this case, a Frame) to be used in their HandleAsync logic
                // Pickers can just use Window.Current.Content as Frame to have a navigation context
                // Background activation does not show any UI so can continue without navigation context
                // We used a Lazy object for this because the navigation context is only relevant after setting the content of the current Window with a Shell or a Frame
                if(activationHandler.Strategy == ActivationStrategy.Normal)
                {
                    activationHandler.SetNavigationContext(navigationContext.Value);
                }
                await activationHandler.Handle(args);
            }

            // If no handler was found or a handler with a normal strategy was found but did not set a content to the navigation context, the default handler kicks in
            if(activationHandler == null || (activationHandler.Strategy == ActivationStrategy.Normal && navigationContext.Value.Content == null))
            {
                defaultHandler.SetNavigationContext(navigationContext.Value);
                await defaultHandler.Handle(args);
            }

            if(args is IActivatedEventArgs)
            {
                // Ensure the current window is active
                Window.Current.Activate();

                // Default activation and All normal activations (exclude Background, Hosted and Custom activations)
                if (activationHandler == null || activationHandler.Strategy == ActivationStrategy.Normal)
                {
                    // Tasks after activation
                    await startup?.Invoke();
                }
            }
        }
    }
}
