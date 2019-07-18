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
    public sealed class LaunchActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        private readonly Dictionary<LaunchSource, LaunchOperation> operations;

        public LaunchActivationHandler(params LaunchOperation[] operations) : base(ActivationStrategy.Normal)
        {
            this.operations = new Dictionary<LaunchSource, LaunchOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Source, operation);
            }
            Operations = new ReadOnlyDictionary<LaunchSource, LaunchOperation>(this.operations);
        }

        public ReadOnlyDictionary<LaunchSource, LaunchOperation> Operations { get; }

        protected override async Task HandleInternal(LaunchActivatedEventArgs args)
        {
            if (args.TileId == "App" && string.IsNullOrEmpty(args.Arguments))
            {
                // Primary Tile
                if(operations.TryGetValue(LaunchSource.Primary, out var operation))
                {
                    await operation.Run(args, NavigationContext);
                }
            }
            else if (!string.IsNullOrEmpty(args.TileId) && args.TileId != "App")
            {
                // Secondary Tile
                if (operations.TryGetValue(LaunchSource.Secondary, out var operation))
                {
                    await operation.Run(args, NavigationContext);
                }
            }
            else if (args.TileId == "App" && !string.IsNullOrEmpty(args.Arguments))
            {
                // Jumplist
                if (operations.TryGetValue(LaunchSource.Jumplist, out var operation))
                {
                    await operation.Run(args, NavigationContext);
                }
            }
            else if (args.TileActivatedInfo != null)
            {
                // Chaseable Tile
                if (operations.TryGetValue(LaunchSource.Chaseable, out var operation))
                {
                    await operation.Run(args, NavigationContext);
                }
            }
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            if (args.TileId == "App" && string.IsNullOrEmpty(args.Arguments))
            {
                // Primary Tile
                return operations.ContainsKey(LaunchSource.Primary);
            }
            else if (!string.IsNullOrEmpty(args.TileId) && args.TileId != "App")
            {
                // Secondary Tile
                return operations.ContainsKey(LaunchSource.Secondary);
            }
            else if (args.TileId == "App" && !string.IsNullOrEmpty(args.Arguments))
            {
                // Jumplist
                return operations.ContainsKey(LaunchSource.Jumplist);
            }
            else if (args.TileActivatedInfo != null)
            {
                // Chaseable Tile
                return operations.ContainsKey(LaunchSource.Chaseable);
            }
            return false;
        }
    }
}
