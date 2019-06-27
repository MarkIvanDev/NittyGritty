using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class DateTimePropertyConfiguration<TOwner> : ComparablePropertyConfiguration<TOwner, DateTime, DateTimePropertyConfiguration<TOwner>>
        where TOwner : class
    {
        public DateTimePropertyConfiguration(Func<TOwner, DateTime> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
