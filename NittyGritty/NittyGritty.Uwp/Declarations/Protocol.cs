using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class Protocol
    {
        public Protocol(string scheme)
        {
            Scheme = scheme;
        }

        public string Scheme { get; }

        public async Task Run(Uri deepLink)
        {
            if(deepLink.Scheme != Scheme)
            {
                return;
            }

            var path = deepLink.LocalPath;
            var parameters = QueryString.Parse(deepLink.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            await Process(Scheme, path, parameters);
        }

        protected abstract Task Process(string scheme, string path, QueryString parameters);
    }
}
