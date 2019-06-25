using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public abstract class PropertyConfiguration<T> : IPropertyConfiguration
    {
        public Type PropertyType => typeof(T);

        private Collection<Func<T, bool>> _validators;

        public Collection<Func<T, bool>> Validators
        {
            get
            {
                return _validators ?? (_validators = new Collection<Func<T, bool>>());
            }
        }

        public virtual PropertyConfiguration<T> Is(Func<T, bool> condition)
        {
            return this;
        }

        public virtual PropertyConfiguration<T> IsNot(Func<T, bool> condition)
        {
            return this;
        }

        public virtual PropertyConfiguration<T> EqualTo(T other)
        {

            return this;
        }
    }
}
