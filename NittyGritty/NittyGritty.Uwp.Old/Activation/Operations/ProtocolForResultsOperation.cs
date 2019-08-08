using NittyGritty.Models;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using NittyGritty.Uwp.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ProtocolForResultsOperation : IViewOperation<ProtocolForResultsActivatedEventArgs>
    {
        private readonly Dictionary<string, ProtocolForResultsConfiguration> configurations;

        /// <summary>
        /// Creates a ProtocolForResultsOperation to handle the protocol that activated the app and returns the results to the caller
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolForResultsOperation can handle.
        /// Cannot be null, empty, or consist of whitespace only.</param>
        public ProtocolForResultsOperation(string scheme, params ProtocolForResultsConfiguration[] configurations)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
            this.configurations = new Dictionary<string, ProtocolForResultsConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.Path, config);
            }
        }

        public string Scheme { get; }

        public virtual async Task Run(ProtocolForResultsActivatedEventArgs args, Frame frame)
        {
            if (args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            var payload = new ProtocolForResultsPayload(path, args.Data, parameters, args.ProtocolForResultsOperation);

            var configuration = GetConfiguration(path);
            configuration.View.Show(payload, frame);
            await Task.CompletedTask;
        }

        private ProtocolForResultsConfiguration GetConfiguration(string path)
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
