using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views.Payloads
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
            Data = data;
            Parameters = parameters;
        }

        public string Path { get; }

        public IDictionary<string, object> Data { get; }

        public QueryString Parameters { get; }
    }
}
