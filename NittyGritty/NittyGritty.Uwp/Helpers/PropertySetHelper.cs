using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace NittyGritty.Uwp.Helpers
{
    public static class PropertySetHelper
    {
        public static string GetPropertyText(this PropertySet set, string property)
        {
            return set.TryGetValue(property, out var p) && p is PropertySet prop && prop.ContainsKey("#text") ?
                prop["#text"].ToString() : null;
        }
    }
}
