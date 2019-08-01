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
        /// <param name="parameters">The query that the protocol passed. Can be empty</param>
        public ProtocolPayload(string path, IDictionary<string, object> data, QueryString parameters)
        {
            Path = path;
            Data = new ReadOnlyDictionary<string, object>(data);
            Parameters = parameters;
        }

        public string Path { get; }

        public IReadOnlyDictionary<string, object> Data { get; }

        public QueryString Parameters { get; }
    }
}
