using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public class ProtocolPayload
    {
        /// <summary>
        /// Creates a Payload for the Protocol
        /// </summary>
        /// <param name="data">The data that the protocol passed. Can be null</param>
        /// <param name="parameter">The query that the protocol passed. Can be empty</param>
        public ProtocolPayload(IDictionary<string, object> data, QueryString parameter)
        {
            Data = data;
            Parameter = parameter;
        }

        public IDictionary<string, object> Data { get; }

        public QueryString Parameter { get; }
    }
}
