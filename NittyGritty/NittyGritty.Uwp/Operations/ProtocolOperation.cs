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
    public class ProtocolOperation : KeyViewOperation<ProtocolPayload>
    {
        /// <summary>
        /// Creates a ProtocolOperation to handle the protocol that activated the app
        /// </summary>
        /// <param name="scheme">The scheme this ProtocolOperation can handle.
        /// Cannot be null or empty or whitespace</param>
        public ProtocolOperation(string scheme) : base()
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty, or whitespace.", nameof(scheme));
            }

            Scheme = scheme;
        }

        public string Scheme { get; }

        public virtual async Task Run(ProtocolActivatedEventArgs args, Frame frame)
        {
            if (args.Uri.Scheme != Scheme)
            {
                return;
            }

            var path = args.Uri.LocalPath;
            var parameters = QueryString.Parse(args.Uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            var payload = new ProtocolPayload(path, args.Data, parameters);

            var configuration = GetConfiguration(path);
            await configuration.Run(payload, args.CurrentlyShownApplicationViewId, frame);
        }
    }
}
