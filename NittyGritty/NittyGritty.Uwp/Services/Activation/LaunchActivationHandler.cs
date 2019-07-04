using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class LaunchActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        public LaunchActivationHandler()
        {
            Handler = async (args) =>
            {
                if (args.TileId == "App" && string.IsNullOrEmpty(args.Arguments))
                {
                    // Primary Tile
                    await Primary?.Invoke(args);
                }
                else if (!string.IsNullOrEmpty(args.TileId) && args.TileId != "App")
                {
                    // Secondary Tile
                    await Secondary?.Invoke(args);
                }
                else if (args.TileId == "App" && !string.IsNullOrEmpty(args.Arguments))
                {
                    // Jumplist
                    await Jumplist?.Invoke(args);
                }
                else if (args.TileActivatedInfo != null)
                {
                    // Chaseable Tile
                    await Chaseable?.Invoke(args);
                }
            };
        }

        public Func<LaunchActivatedEventArgs, Task> Primary { get; set; }

        public Func<LaunchActivatedEventArgs, Task> Secondary { get; set; }

        public Func<LaunchActivatedEventArgs, Task> Jumplist { get; set; }

        public Func<LaunchActivatedEventArgs, Task> Chaseable { get; set; }
    }
}
