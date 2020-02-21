using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Data
{
    public class ToastPayload
    {
        public ToastPayload(IDictionary<string, object> data, QueryString parameters)
        {
            Data = new ReadOnlyDictionary<string, object>(data);
            Parameters = parameters;
        }

        public IReadOnlyDictionary<string, object> Data { get; }

        public QueryString Parameters { get; }
    }
}
