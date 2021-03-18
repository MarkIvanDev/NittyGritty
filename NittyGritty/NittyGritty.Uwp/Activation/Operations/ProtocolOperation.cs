using NittyGritty.Models;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ProtocolOperation : IViewOperation<ProtocolActivatedEventArgs>
    {
        private readonly Dictionary<string, ProtocolConfiguration> configurations;

        /// <summary>
        /// Creates a ProtocolOperation to handle the protocol that activated the app
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolOperation can handle.
        /// Cannot be null or empty or whitespace</param>
        public ProtocolOperation(string scheme, params ProtocolConfiguration[] configurations)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
            this.configurations = new Dictionary<string, ProtocolConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.Path, config);
            }
        }

        public string Scheme { get; }

        public virtual async Task Run(ProtocolActivatedEventArgs args, Frame frame)
        {
            if (args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var payload = new ProtocolPayload(args.Uri, args.Data);

            var config = GetConfiguration(path);
            await config.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
        }

        private ProtocolConfiguration GetConfiguration(string path)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(path, out var config))
                {
                    return config;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackConfig))
                    {
                        return fallbackConfig;
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"No configuration for path: {path}. Did you forget to register a ProtocolConfiguration?",
                            nameof(path));
                    }
                }
            }
        }
    }
}
