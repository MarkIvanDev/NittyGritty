using NittyGritty.Uwp.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class ProtocolActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        private readonly List<Protocol> protocols = new List<Protocol>();

        public ProtocolActivationHandler(IEnumerable<Protocol> protocols)
        {
            foreach (var protocol in protocols)
            {
                if(this.protocols.Any(p => p.Scheme == protocol.Scheme))
                {
                    // You only have to register for a scheme once
                    continue;
                }
                this.protocols.Add(protocol);
            }
        }

        public Func<Uri, Task> UnknownProtocol { get; set; }

        public override async Task HandleAsync(ProtocolActivatedEventArgs args)
        {
            var protocol = protocols.SingleOrDefault(p => p.Scheme == args.Uri.Scheme);
            if(protocol == null)
            {
                await UnknownProtocol?.Invoke(args.Uri);
                return;
            }
            await protocol.Run(args.Uri);
        }
    }
}
