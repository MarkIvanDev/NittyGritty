using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class EntityPropertyConfiguration<TOwner, TProperty> : BasePropertyConfiguration<TOwner, TProperty, EntityPropertyConfiguration<TOwner, TProperty>>
        where TOwner : class
        where TProperty : class
    {
        public EntityPropertyConfiguration(Func<TOwner, TProperty> propertyFunc) : base(propertyFunc)
        {
        }
    }
}
