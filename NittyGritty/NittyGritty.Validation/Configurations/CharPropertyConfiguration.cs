using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class CharPropertyConfiguration<TOwner> : ComparablePropertyConfiguration<TOwner, char, CharPropertyConfiguration<TOwner>>
        where TOwner : class
    {
        public CharPropertyConfiguration(Func<TOwner, char> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
