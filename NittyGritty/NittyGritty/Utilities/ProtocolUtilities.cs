using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class ProtocolUtilities
    {
        public static Uri Create(string scheme, string path, QueryString parameters)
        {
            if(string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty or whitespace", nameof(scheme));
            }

            return new Uri($"{scheme}:{path}?{parameters?.ToString()}");
        }
    }
}
