using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IProtocolForResultsPayload
    {
        string Path { get; }

        IReadOnlyDictionary<string, object> Data { get; }

        QueryString Parameters { get; }

        void ReportResults(IDictionary<string, object> results);
    }
}
