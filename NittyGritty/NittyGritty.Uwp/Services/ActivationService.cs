using NittyGritty.Uwp.Services.Activation;
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
        private readonly IEnumerable<ActivationHandler<IActivatedEventArgs>> handlers;
        private readonly Lazy<UIElement> shell;
        private readonly Func<Task> startup;

        public ActivationService(Application app,
                                 Func<Task> initialization,
                                 IEnumerable<ActivationHandler<IActivatedEventArgs>> handlers,
                                 Lazy<UIElement> shell,
                                 Func<Task> startup)
        {
            if (!handlers.Any())
            {
                throw new Exception("Activation Handlers can not be empty");
            }

            this.app = app;
            this.initialization = initialization;
            this.handlers = handlers;
            this.shell = shell;
            this.startup = startup;
        }

        public async Task ActivateAsync(object args)
        {
            if (args is IActivatedEventArgs e)
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

                ActivationHandler<IActivatedEventArgs> activationHandler = null;
                foreach (var handler in handlers)
                {
                    if (handler.CanHandle(e))
                    {
                        activationHandler = handler;
                        break;
                    }
                }

                if (activationHandler != null)
                {
                    await activationHandler.HandleAsync(e);
                }
                else
                {
                    throw new Exception("No Activation handler detected");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

            // Tasks after activation
            await startup.Invoke();
        }

    }
}
