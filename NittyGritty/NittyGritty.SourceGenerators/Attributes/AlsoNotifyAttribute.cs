using System;

namespace NittyGritty.SourceGenerators.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public sealed class AlsoNotifyAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
