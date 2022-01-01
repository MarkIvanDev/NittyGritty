using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class ProtocolConfiguration
    {
        /// <summary>
        /// Creates a protocol configuration
        /// </summary>
        /// <param name="path">Sets the path for this protocol configuration. Path can also be empty or *.
        /// A path with a value of * will be used for all other unknown paths</param>
        /// <param name="view"></param>
        public ProtocolConfiguration(string path, MultiViewConfiguration<ProtocolPayload> view)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.Trim().Length == 0 && !path.Equals(string.Empty))
            {
                throw new ArgumentException("Path cannot consist of whitespace only");
            }

            Path = path;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public string Path { get; }

        public MultiViewConfiguration<ProtocolPayload> View { get; }
    }
}
