using NittyGritty.Models;
using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class ProtocolOperation
    {
        private readonly Dictionary<string, ProtocolPathConfiguration> _pathConfigurations = new Dictionary<string, ProtocolPathConfiguration>();

        /// <summary>
        /// Creates a ProtocolOperation to handle the protocol that activated the app
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolOperation can handle.
        /// Cannot be null or empty or whitespace</param>
        public ProtocolOperation(string scheme)
        {
            if(string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
        }

        public string Scheme { get; }

        /// <summary>
        /// Configures the paths that this scheme can handle with the appropriate view
        /// </summary>
        /// <param name="path">The path that this scheme can handle. A path can be configured with an empty string
        /// A path with a value of * will be used as fallback for unknown paths</param>
        /// <param name="view">The type of the view that the path leads to</param>
        public void Configure(string path, Type view, Predicate<QueryString> createsNewView = null)
        {
            lock (_pathConfigurations)
            {
                if (_pathConfigurations.ContainsKey(path))
                {
                    throw new ArgumentException("This path is already used: " + path);
                }

                if (view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                var configuration = new ProtocolPathConfiguration(path, view, createsNewView);

                var existing = _pathConfigurations.GetValueOrDefault(path);
                if (existing != null)
                {
                    throw new ArgumentException(
                        "This configuration already exists with path " + existing.Path);
                }

                _pathConfigurations.Add(
                    path,
                    configuration);
            }
        }

        public virtual async Task Run(ProtocolActivatedEventArgs args, Frame frame)
        {
            if(args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            var payload = new ProtocolPayload(args.Data, parameters);

            ProtocolPathConfiguration pathConfiguration = null;
            lock (_pathConfigurations)
            {
                if(_pathConfigurations.TryGetValue(path, out var view))
                {
                    pathConfiguration = view;
                }
                else
                {
                    if(_pathConfigurations.TryGetValue("*", out var fallbackView))
                    {
                        pathConfiguration = fallbackView;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No such path: {0}. Did you forget to call Protocol.Configure?",
                                path),
                            nameof(path));
                    }
                }
            }

            if(pathConfiguration.CreatesNewView(parameters))
            {
                var newView = CoreApplication.CreateNewView();
                int newViewId = 0;
                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var newViewFrame = new Frame();
                    newViewFrame.Navigate(pathConfiguration.View, payload);
                    Window.Current.Content = newViewFrame;
                    // You have to activate the window in order to show it later.
                    Window.Current.Activate();

                    newViewId = ApplicationView.GetForCurrentView().Id;
                });

                if (args.CurrentlyShownApplicationViewId != 0)
                {
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                        newViewId,
                        ViewSizePreference.Default,
                        args.CurrentlyShownApplicationViewId,
                        ViewSizePreference.Default);
                }
                else
                {
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                }
            }
            else
            {
                frame.Navigate(pathConfiguration.View, payload);
            }
        }
    }
}
