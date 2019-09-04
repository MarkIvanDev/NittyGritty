using NittyGritty.Models;
using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.System;

namespace NittyGritty.Uwp.Payloads
{
    public class ProtocolForResultsPayload : IProtocolForResultsPayload
    {
        private readonly ProtocolForResultsOperation operation;

        public ProtocolForResultsPayload(string path, IDictionary<string, object> data, QueryString parameters, ProtocolForResultsOperation operation)
        {
            Path = path;
            Data = data != null ? new ReadOnlyDictionary<string, object>(data) : null;
            Parameters = parameters;
            this.operation = operation;
        }

        public string Path { get; }

        public IReadOnlyDictionary<string, object> Data { get; }

        public QueryString Parameters { get; }

        public void ReportResults(IDictionary<string, object> results)
        {
            var data = new ValueSet();
            foreach (var item in results)
            {
                data.Add(item.Key, item.Value);
            }
            operation.ReportCompleted(data);
        }
    }
}
