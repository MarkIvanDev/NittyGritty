using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class DateTimeOffsetPropertyConfiguration<TOwner> : ComparablePropertyConfiguration<TOwner, DateTimeOffset, DateTimeOffsetPropertyConfiguration<TOwner>>
        where TOwner : class
    {
        public DateTimeOffsetPropertyConfiguration(Func<TOwner, DateTimeOffset> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
