using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class BoolPropertyConfiguration<TOwner> : ComparablePropertyConfiguration<TOwner, bool, BoolPropertyConfiguration<TOwner>>
        where TOwner : class
    {
        public BoolPropertyConfiguration(Func<TOwner, bool> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
