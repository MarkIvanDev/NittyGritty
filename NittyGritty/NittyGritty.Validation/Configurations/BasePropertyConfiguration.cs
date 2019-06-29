using NittyGritty.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public abstract class BasePropertyConfiguration<TOwner, TProperty, TConfig> : ObservableObject, IPropertyConfiguration<TOwner, TProperty>
        where TOwner : class
        where TConfig : BasePropertyConfiguration<TOwner, TProperty, TConfig>
    {
        
        public BasePropertyConfiguration(Func<TOwner, TProperty> propertyFunc)
        {
            PropertyFunc = propertyFunc;
        }

        public Collection<string> Triggers { get; } = new Collection<string>();

        public Collection<Validator<TProperty>> Validators { get; } = new Collection<Validator<TProperty>>();

        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public Func<TOwner, TProperty> PropertyFunc { get; }

        async Task IPropertyConfiguration.ValidateAsync(object owner)
        {
            if(owner is TOwner o)
            {
                await ValidateAsync(o);
            }
            else
            {
                throw new ArgumentException($"Parameter is not of type {typeof(TOwner).ToString()}", nameof(owner));
            }
        }

        public virtual async Task ValidateAsync(TOwner owner)
        {
            foreach (var validator in Validators)
            {
                var property = PropertyFunc(owner);
                var result = await validator.ValidateAsync(property);
                if(!result.IsValid)
                {
                    Errors.Add(result.Error);
                }
            }
        }

        public TConfig Is(Func<TProperty, bool> condition, string error = null)
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
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig Is(Func<TProperty, Task<bool>> condition, string error = null)
        {
            if (error == null)
            {
                error = $"Must satisfy the given condition";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, Task<ValidationResult>>(
                    async (prop) =>
                    {
                        var result = await condition(prop);
                        return new ValidationResult(result, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig IsNot(Func<TProperty, bool> condition, string error = null)
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
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig IsNot(Func<TProperty, Task<bool>> condition, string error = null)
        {
            if (error == null)
            {
                error = $"Must not satisfy the given condition";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, Task<ValidationResult>>(
                    async (prop) =>
                    {
                        var result = await condition(prop);
                        return new ValidationResult(!result, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig Null(string error = null)
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
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig NotNull(string error = null)
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
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig DependsOn<TOtherProperty>(Expression<Func<TOwner, TOtherProperty>> property, string error = null)
        {
            Triggers.Add(ExpressionUtilities.GetPropertyName(property));
            return this as TConfig;
        }

    }
}
