using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ProgressInfo
    {
        public ProgressInfo(long? total, long value, string name = null)
        {
            Total = total;
            Value = value;
            Name = name;
        }

        public long? Total { get; }

        public long Value { get; }

        public string Name { get; }
    }
}
