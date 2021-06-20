using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public class ProtocolPayload
    {
        /// <summary>
        /// Creates a Payload for the Protocol
        /// </summary>
        /// <param name="data">The data that the protocol passed. Can be null</param>
        public ProtocolPayload(Uri uri, IDictionary<string, object> data)
        {
            Uri = uri;
            Data = data;
        }

        public Uri Uri { get; }

        public IDictionary<string, object> Data { get; }

    }
}
