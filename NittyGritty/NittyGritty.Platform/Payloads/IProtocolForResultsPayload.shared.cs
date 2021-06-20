using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IProtocolForResultsPayload
    {
        Uri Uri { get; }

        IDictionary<string, object> Data { get; }

        void ReportResults(IDictionary<string, object> results);
    }
}
