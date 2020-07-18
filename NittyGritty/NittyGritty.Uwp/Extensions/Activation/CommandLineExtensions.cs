using System;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Payloads;
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
