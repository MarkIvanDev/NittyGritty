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

        public Collection<Validator<T>> Validators { get; } = new Collection<Validator<T>>();

        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public async Task ValidateAsync(T property)
        {
            foreach (var validator in Validators)
            {
                var result = await validator.ValidateAsync(property);
                if(!result.IsValid)
                {
                    Errors.Add(result.Error);
                }
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
