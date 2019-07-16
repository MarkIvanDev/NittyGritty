using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class Protocol
    {
        private readonly Dictionary<string, Type> _viewsByPath = new Dictionary<string, Type>();

        public Protocol(string scheme)
        {
            Scheme = scheme;
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

        public async Task Run(Uri deepLink, Frame frame)
        {
            if(deepLink.Scheme != Scheme)
            {
                return;
            }

            var path = deepLink.LocalPath;
            var parameters = QueryString.Parse(deepLink.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            lock (_viewsByPath)
            {
                if (!_viewsByPath.ContainsKey(path))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such path: {0}. Did you forget to call Protocol.Configure?",
                            path),
                        nameof(path));
                }

                frame.Navigate(_viewsByPath[path], parameters);
            }
            await Task.CompletedTask;
        }
    }
}
