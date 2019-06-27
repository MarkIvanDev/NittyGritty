using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class NumericPropertyConfiguration<TOwner, TProperty> : ComparablePropertyConfiguration<TOwner, TProperty, NumericPropertyConfiguration<TOwner, TProperty>>
        where TOwner : class
        where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
    {
        public NumericPropertyConfiguration(Func<TOwner, TProperty> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
