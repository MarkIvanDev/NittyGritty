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
    public class ProtocolForResultsActivationHandler : ActivationHandler<ProtocolForResultsActivatedEventArgs>
    {
        private static readonly Dictionary<string, ProtocolForResults> _protocols;

        static ProtocolForResultsActivationHandler()
        {
            _protocols = new Dictionary<string, ProtocolForResults>();
        }

        public ProtocolForResultsActivationHandler()
        {
            Strategy = ActivationStrategy.Picker;
        }

        public static ReadOnlyDictionary<string, ProtocolForResults> Protocols
        {
            get { return new ReadOnlyDictionary<string, ProtocolForResults>(_protocols); }
        }

        public static void AddProtocol(ProtocolForResults protocol)
        {
            if (!_protocols.ContainsKey(protocol.Scheme))
            {
                _protocols.Add(protocol.Scheme, protocol);
            }
            else
            {
                throw new ArgumentException("You only have to register for a protocol once.");
            }
        }

        public override async Task HandleAsync(ProtocolForResultsActivatedEventArgs args)
        {
            if (_protocols.TryGetValue(args.Uri.Scheme, out var protocol))
            {
                await protocol.Run(args.Uri);
            }
            else
            {
                await UnknownProtocol?.Invoke(args.Uri);
            }
        }
    }
}
