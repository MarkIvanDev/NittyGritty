using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class ProtocolOperation
    {
        private readonly Dictionary<string, Type> _viewsByPath = new Dictionary<string, Type>();

        /// <summary>
        /// Creates a ProtocolOperation to handle the protocol that activated the app
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolOperation can handle. Cannot be null or empty or whitespace</param>
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
        /// <param name="path">The path that this scheme can handle. A path can be configured with an empty string</param>
        /// <param name="view">The type of the view that the path leads to</param>
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

        public virtual async Task Run(ProtocolActivatedEventArgs args, Frame frame)
        {
            if(args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            lock (_viewsByPath)
            {
                if(_viewsByPath.TryGetValue(path, out var view))
                {
                    frame.Navigate(view, parameters);
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
