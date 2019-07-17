using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Declarations
{
    public class ProtocolForResults
    {
        private readonly Dictionary<string, Type> _viewsByPath = new Dictionary<string, Type>();

        public ProtocolForResults(string scheme)
        {
            Scheme = scheme;
        }

        public string Scheme { get; }

        public void Configure(string path, Type view)
        {
            lock (_viewsByPath)
            {
                if (_viewsByPath.ContainsKey(path))
                {
                    throw new ArgumentException("This path is already used: " + path);
                }

                if (_viewsByPath.Any(p => p.Value == view))
                {
                    throw new ArgumentException(
                        "This view is already configured with path " + _viewsByPath.First(p => p.Value == view).Key);
                }

                _viewsByPath.Add(
                    path,
                    view);
            }
        }

        public async Task Run(Uri deepLink, )
        {

        }
    }
}
