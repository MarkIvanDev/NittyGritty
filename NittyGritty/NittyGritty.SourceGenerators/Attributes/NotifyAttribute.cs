using System;
using System.ComponentModel;
using System.Reflection;

namespace NittyGritty.SourceGenerators.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class NotifyAttribute : Attribute
    {
        public string Name { get; set; }

        public AccessLevel Getter { get; set; }

        public AccessLevel Setter { get; set; }
    }

    public enum AccessLevel
    {
        [Description("")]
        Public = 0,

        [Description("protected internal ")]
        ProtectedInternal = 1,

        [Description("internal ")]
        Internal = 2,

        [Description("protected ")]
        Protected = 3,

        [Description("private protected ")]
        PrivateProtected = 4,

        [Description("private ")]
        Private = 5,

    }

    internal static class AccessLevelExtensions
    {
        public static string GetDescription(this AccessLevel accessLevel)
        {
            var name = accessLevel.ToString();
            return typeof(AccessLevel)
                .GetField(name)?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description ?? name;
        }
    }
}
