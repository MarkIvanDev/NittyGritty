using NittyGritty.Uwp.Declarations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class ProtocolActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        private static readonly Dictionary<string, Protocol> _protocols;

        static ProtocolActivationHandler()
        {
            _protocols = new Dictionary<string, Protocol>();
        }

        public ProtocolActivationHandler()
        {
            NeedsNavigationContext = true;
        }

        public static ReadOnlyDictionary<string, Protocol> Protocols
        {
            get { return new ReadOnlyDictionary<string, Protocol>(_protocols); }
        }

        public static void AddProtocol(Protocol protocol)
        {
            if (!_protocols.ContainsKey(protocol.Scheme))
            {
                _protocols.Add(protocol.Scheme, protocol);
            }
            else
            {
                throw new ArgumentException("You only have to register for a file type association once.");
            }
        }

        public Func<Uri, Task> UnknownProtocol { get; set; }

        public override async Task HandleAsync(ProtocolActivatedEventArgs args)
        {
            if(_protocols.TryGetValue(args.Uri.Scheme, out var protocol))
            {
                await protocol.Run(args.Uri, NavigationContext);
            }
            else
            {
                await UnknownProtocol?.Invoke(args.Uri);
            }
        }
    }
}
