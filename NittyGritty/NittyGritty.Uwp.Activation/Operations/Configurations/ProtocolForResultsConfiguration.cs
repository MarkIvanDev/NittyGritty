using System;
using System.Text;
using NittyGritty.Platform.Payloads;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class ProtocolForResultsConfiguration
    {
        public ProtocolForResultsConfiguration(string path, SingleViewConfiguration<ProtocolForResultsPayload> view)
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

        public SingleViewConfiguration<ProtocolForResultsPayload> View { get; }
    }
}
