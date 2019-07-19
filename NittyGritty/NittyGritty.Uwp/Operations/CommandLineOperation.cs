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
        public CommandLineOperation(string command, Type view)
        {
            Command = command;
            View = view;
        }

        public string Command { get; }

        public Type View { get; }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            
        }
    }
}
