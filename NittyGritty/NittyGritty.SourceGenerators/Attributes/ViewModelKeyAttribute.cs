using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.SourceGenerators.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewModelKeyAttribute : Attribute
    {
        public string Key { get; set; }
    }
}
