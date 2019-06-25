using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public abstract class NumericPropertyConfiguration<TConfig, TProperty> : PropertyConfiguration<TProperty>
        where TConfig : class, IPropertyConfiguration
        where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
    {
        public TConfig IsGreaterThan(TProperty other)
        {
            var func = new Func<TProperty, bool>((prop) => prop.CompareTo(other) > 0);
            this.Validators.Add(func);
            return this as TConfig;
        }
    }
}
