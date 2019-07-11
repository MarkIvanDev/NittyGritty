using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class CommandLineActivationHandler : ActivationHandler<CommandLineActivatedEventArgs>
    {
        public CommandLineActivationHandler()
        {

        }

        public override async Task HandleAsync(CommandLineActivatedEventArgs args)
        {
            
        }
    }
}
