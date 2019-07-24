using NittyGritty.Models;
using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class ProtocolForResultsOperation
    {
        private readonly Dictionary<string, Type> _viewsByPath = new Dictionary<string, Type>();
        private readonly Func<ProtocolForResultsActivatedEventArgs, Task> callback;

        /// <summary>
        /// Creates a ProtocolForResultsOperation to handle the protocol that activated the app and returns the results to the caller
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolForResultsOperation can handle.
        /// Cannot be null, empty, or consist of whitespace only.</param>
        /// <param name="callback"></param>
        public ProtocolForResultsOperation(string scheme, Func<ProtocolForResultsActivatedEventArgs, Task> callback)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
            this.callback = callback;
        }

        public string Scheme { get; }

        /// <summary>
        /// Configures the paths that this scheme can handle with the appropriate view
        /// </summary>
        /// <param name="path">The path that this scheme can handle. A path can be configured with an empty string
        /// A path with a value of * will be used as fallback for unknown paths</param>
        /// <param name="view"></param>
        public void Configure(string path, Type view)
        {
            lock (_viewsByPath)
            {
                if (_viewsByPath.ContainsKey(path))
                {
                    throw new ArgumentException("This path is already used: " + path);
                }

                if(view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                if (_viewsByPath.Any(p => p.Value == view))
                {
                    throw new ArgumentException(
                        "This view is already configured with path " + _viewsByPath.First(p => p.Value == view).Key);
                }

                _viewsByPath.Add(
                    path,
                    view);
            }
        }

        public virtual async Task Run(ProtocolForResultsActivatedEventArgs args)
        {
            if (args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            var payload = new ProtocolPayload(args.Data, parameters);
            lock (_viewsByPath)
            {
                if (_viewsByPath.TryGetValue(path, out var view))
                {
                    if(Window.Current.Content is Frame frame)
                    {
                        frame.Navigate(view, payload);
                    }
                }
                else
                {
                    if(_viewsByPath.TryGetValue("*", out var fallbackView))
                    {
                        if (Window.Current.Content is Frame frame)
                        {
                            frame.Navigate(fallbackView, payload);
                        }
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
            await Task.CompletedTask;
        }
    }
}
