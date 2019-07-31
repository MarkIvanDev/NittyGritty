using NittyGritty.Models;
using NittyGritty.Views.Payloads;
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
    public class ProtocolForResultsOperation : HostedOperation<ProtocolPayload>
    {
        private readonly Dictionary<string, Type> _viewsByPath = new Dictionary<string, Type>();

        /// <summary>
        /// Creates a ProtocolForResultsOperation to handle the protocol that activated the app and returns the results to the caller
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolForResultsOperation can handle.
        /// Cannot be null, empty, or consist of whitespace only.</param>
        public ProtocolForResultsOperation(string scheme)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
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
            var payload = new ProtocolPayload(path, args.Data, parameters);

            var configuration = GetConfiguration(path);
            configuration.Run(payload, frame);
            await Task.CompletedTask;
        }
    }
}
