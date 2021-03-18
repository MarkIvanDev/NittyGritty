using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NittyGritty.Models;
using Windows.Foundation.Collections;
using Windows.System;

namespace NittyGritty.Platform.Payloads
{
    public class ProtocolForResultsPayload : IProtocolForResultsPayload
    {
        private readonly ProtocolForResultsOperation operation;

        public ProtocolForResultsPayload(Uri uri, IDictionary<string, object> data, ProtocolForResultsOperation operation)
        {
            Uri = uri;
            Data = data != null ? new ReadOnlyDictionary<string, object>(data) : null;
            this.operation = operation;
        }

        public Uri Uri { get; }

        public IReadOnlyDictionary<string, object> Data { get; }

        public void ReportResults(IDictionary<string, object> results)
        {
            if (results is null)
            {
                operation.ReportCompleted(null);
            }
            else
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
}
