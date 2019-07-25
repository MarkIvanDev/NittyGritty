using NittyGritty.Models;
using NittyGritty.Views;
using NittyGritty.Views.Payloads;
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
        private readonly Dictionary<string, KeyViewConfiguration> configurations;

        /// <summary>
        /// Creates a ProtocolOperation to handle the protocol that activated the app
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolOperation can handle.
        /// Cannot be null or empty or whitespace</param>
        public ProtocolOperation(string scheme)
        {
            configurations = new Dictionary<string, KeyViewConfiguration>();

            if (string.IsNullOrWhiteSpace(scheme))
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
            lock (configurations)
            {
                if (path.Trim().Length == 0 && !path.Equals(string.Empty))
                {
                    throw new ArgumentException("Path cannot consist of whitespace only");
                }

                if (configurations.ContainsKey(path))
                {
                    throw new ArgumentException("This path is already used: " + path);
                }

                if (view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                var configuration = new KeyViewConfiguration(path, view, createsNewView);

                configurations.Add(
                    path,
                    configuration);
            }
        }

        public virtual async Task Run(ProtocolActivatedEventArgs args, Frame frame)
        {
            if (args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            var payload = new ProtocolPayload(path, args.Data, parameters);

            KeyViewConfiguration configuration = null;
            lock (configurations)
            {
                if (configurations.TryGetValue(path, out var view))
                {
                    configuration = view;
                }
                else
                {
                    if(configurations.TryGetValue("*", out var fallbackView))
                    {
                        configuration = fallbackView;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No configuration for path: {0}. Did you forget to call Protocol.Configure?",
                                path),
                            nameof(path));
                    }
                }
            }

            await configuration.Run(parameters, payload, args.CurrentlyShownApplicationViewId, frame);
        }
    }
}
