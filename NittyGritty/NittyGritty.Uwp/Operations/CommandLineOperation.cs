using NittyGritty.Models;
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
        private readonly Dictionary<string, PathConfiguration> _pathConfigurations;

        public CommandLineOperation(string alias)
        {
            _pathConfigurations = new Dictionary<string, PathConfiguration>();

            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException("Alias cannot be null, empty, or whitespace.", nameof(alias));
            }

            Alias = alias;
        }

        public string Alias { get; }

        /// <summary>
        /// Configures the commands that this alias can handle with the appropriate view
        /// </summary>
        /// <param name="command">The command that this alias can handle. A command can be configured with an empty string
        /// A command with a value of * will be used as fallback for unknown commands</param>
        /// <param name="view">The type of the view that the path leads to</param>
        public void Configure(string command, Type view, Predicate<QueryString> createsNewView = null)
        {
            lock (_pathConfigurations)
            {
                if (command.Trim().Length == 0 && !command.Equals(string.Empty))
                {
                    throw new ArgumentException("Command cannot consist of whitespace only");
                }

                if (_pathConfigurations.ContainsKey(command))
                {
                    throw new ArgumentException("This command is already used: " + command);
                }

                if (view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                var configuration = new PathConfiguration(command, view, createsNewView);

                _pathConfigurations.Add(
                    command,
                    configuration);
            }
        }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            var deferral = args.Operation.GetDeferral();

            var currentDirectory = args.Operation.CurrentDirectoryPath;


            PathConfiguration pathConfiguration = null;
            lock (_pathConfigurations)
            {
                if (_pathConfigurations.TryGetValue(path, out var view))
                {
                    pathConfiguration = view;
                }
                else
                {
                    if (_pathConfigurations.TryGetValue("*", out var fallbackView))
                    {
                        pathConfiguration = fallbackView;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No such path: {0}. Did you forget to call Protocol.Configure?",
                                path),
                            nameof(path));
                    }
                }
            }

            deferral.Complete();
        }
    }
}
