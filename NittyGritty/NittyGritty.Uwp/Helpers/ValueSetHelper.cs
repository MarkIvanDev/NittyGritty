using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace NittyGritty.Uwp.Helpers
{
    public static class ValueSetHelper
    {
        public static T GetValue<T>(this ValueSet set, string key, T defaultValue = default)
        {
            return set.TryGetValue(key, out var v) && v is T value ? value : defaultValue;
        }
    }
}
