using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.UI.Interactivity.Actions
{
    public enum Operators
    {
        EqualTo = 0,
        NotEqualTo = 1,
        LessThan = 2,
        LessThanOrEqualTo = 3,
        GreaterThan = 4,
        GreaterThanOrEqualTo = 5,
        IsNull = 6,
        IsNotNull = 7,
        IsTrue = 8,
        IsFalse = 9,
        IsNullOrEmpty = 10,
        IsNotNullOrEmpty = 11
    }
}
