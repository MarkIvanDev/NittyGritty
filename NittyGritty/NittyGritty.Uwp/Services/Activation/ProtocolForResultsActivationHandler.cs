using NittyGritty.Uwp.Operations;
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
        private static readonly Dictionary<string, ProtocolForResultsOperation> _protocols;

        static ProtocolForResultsActivationHandler()
        {
            _protocols = new Dictionary<string, ProtocolForResultsOperation>();
        }

        public ProtocolForResultsActivationHandler()
        {
            Strategy = ActivationStrategy.Hosted;
        }

        public static ReadOnlyDictionary<string, ProtocolForResultsOperation> Protocols
        {
            get { return new ReadOnlyDictionary<string, ProtocolForResultsOperation>(_protocols); }
        }

        public static void AddProtocol(ProtocolForResultsOperation protocol)
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

        public override async Task HandleInternal(ProtocolForResultsActivatedEventArgs args)
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
