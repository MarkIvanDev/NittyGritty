using NittyGritty.Uwp.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace NittyGritty.Uwp.Extensions.Activation
{
    public static class CommandLineExtensions
    {
        public static async Task<CommandLinePayload> GetPayload(this CommandLineActivatedEventArgs args)
        {
            var currentDirectory = await StorageFolder.GetFolderFromPathAsync(args.Operation.CurrentDirectoryPath);
            return new CommandLinePayload(args.Operation, currentDirectory);
        }
    }
}
