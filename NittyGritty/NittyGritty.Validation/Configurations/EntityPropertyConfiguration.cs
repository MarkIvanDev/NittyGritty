using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class EntityPropertyConfiguration<T> : PropertyConfiguration<EntityPropertyConfiguration<T>, T> where T : class
    {
    }
}
