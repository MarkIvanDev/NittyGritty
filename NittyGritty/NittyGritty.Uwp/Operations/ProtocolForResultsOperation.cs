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
        /// <param name="scheme">The scheme this ProtocolForResultsOperation can handle. Cannot be null or empty or whitespace</param>
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

        public void Configure(string path, Type view)
        {
            lock (_viewsByPath)
            {
                if (_viewsByPath.ContainsKey(path))
                {
                    throw new ArgumentException("This path is already used: " + path);
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
            lock (_viewsByPath)
            {
                if (_viewsByPath.TryGetValue(path, out var view))
                {
                    if(Window.Current.Content is Frame frame)
                    {
                        var payload = new ProtocolPayload(args.Data, parameters);
                        frame.Navigate(view, payload);
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
            await Task.CompletedTask;
        }
    }
}
