using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class CommandLineOperation
    {
        private readonly Func<CommandLineActivatedEventArgs, Frame, Task> callback;

        public CommandLineOperation(Func<CommandLineActivatedEventArgs, Frame, Task> callback = null)
        {
            this.callback = callback;
        }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            await callback?.Invoke(args, frame);
        }
    }
}
