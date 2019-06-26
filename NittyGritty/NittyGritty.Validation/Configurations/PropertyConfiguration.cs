using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public abstract class PropertyConfiguration<TConfig, TProperty> : IPropertyConfiguration
        where TConfig : class, IPropertyConfiguration
    {
        public Type PropertyType => typeof(TProperty);

        public Collection<Validator<TProperty>> Validators { get; } = new Collection<Validator<TProperty>>();

        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public async Task ValidateAsync(TProperty property)
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

        public virtual TConfig Is(Func<TProperty, bool> condition, string error = null)
        {
            if (error == null)
            {
                error = $"Must satisfy the given condition";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(condition(prop), error);
                    }));
            this.Validators.Add(validator);
            return this as TConfig;
        }

        public virtual TConfig IsNot(Func<TProperty, bool> condition, string error = null)
        {
            if (error == null)
            {
                error = $"Must not satisfy the given condition";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(!condition(prop), error);
                    }));
            this.Validators.Add(validator);
            return this as TConfig;
        }

        public virtual TConfig Null(string error = null)
        {
            if (error == null)
            {
                error = $"Must be null";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop == null, error);
                    }));
            this.Validators.Add(validator);
            return this as TConfig;
        }

        public virtual TConfig NotNull(string error = null)
        {
            if (error == null)
            {
                error = $"Must not be null";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop != null, error);
                    }));
            this.Validators.Add(validator);
            return this as TConfig;
        }

        public virtual TConfig DependsOn()
        {
            return this as TConfig;
        }
    }
}
