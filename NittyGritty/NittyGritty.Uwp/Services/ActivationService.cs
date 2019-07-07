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
        private readonly Application app;
        private readonly Func<Task> initialization;
        private readonly IEnumerable<IActivationHandler> handlers;
        private readonly Lazy<DefaultActivationHandler> defaultHandler;
        private readonly Lazy<UIElement> shell;
        private readonly Func<Task> startup;

        public ActivationService(Application app,
                                 Func<Task> initialization,
                                 Lazy<UIElement> shell,
                                 IEnumerable<IActivationHandler> handlers,
                                 Lazy<DefaultActivationHandler> defaultHandler,
                                 Func<Task> startup)
        {
            if (!handlers.Any() && defaultHandler == null)
            {
                throw new Exception("Activation Handlers can not be empty");
            }

            this.app = app;
            this.initialization = initialization;
            this.handlers = handlers;
            this.defaultHandler = defaultHandler;
            this.shell = shell;
            this.startup = startup;
        }

        public ActivationService(App app) : this(
            app, app.Initialization, new Lazy<UIElement>(app.CreateShell), app.GetActivationHandlers(), new Lazy<DefaultActivationHandler>(app.GetDefaultHandler), app.Startup)
        {

        }

        public async Task ActivateAsync(object args)
        {
            if (args is IActivatedEventArgs)
            {   
                if(args is ShareTargetActivatedEventArgs)
                {
                    Window.Current.Content = new Frame();
                }
                else
                {
                    // Initialize things like registering background task before the app is loaded
                    await initialization.Invoke();

                    // Do not repeat app initialization when the Window already has content,
                    // just ensure that the window is active
                    if (Window.Current.Content == null)
                    {
                        // Create a Frame to act as the navigation context and navigate to the first page
                        Window.Current.Content = shell?.Value ?? new Frame();
                    }
                }
            }

            var activationHandler = handlers.FirstOrDefault(h => h.CanHandle(args));
            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(args);
            }
            else
            {
                await defaultHandler?.Value.HandleAsync(args);
            }

            if(args is IActivatedEventArgs)
            {
                // Ensure the current window is active
                Window.Current.Activate();

                if(!(args is ShareTargetActivatedEventArgs))
                {
                    // Tasks after activation
                    await startup.Invoke();
                }
            }
        }

    }
}
