using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public abstract class ComparablePropertyConfiguration<TOwner, TProperty, TConfig> : BasePropertyConfiguration<TOwner, TProperty, TConfig>
        where TOwner : class
        where TProperty : IComparable<TProperty>
        where TConfig : BasePropertyConfiguration<TOwner, TProperty, TConfig>
    {
        public ComparablePropertyConfiguration(Func<TOwner, TProperty> propertyFunc) : base(propertyFunc)
        {
        }

        public TConfig GreaterThan(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must be greater than {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) > 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig GreaterThanOrEqualTo(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must be greater than or equal to {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) >= 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig LesserThan(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must be lesser than {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) < 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig LesserThanOrEqualTo(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must be lesser than or equal to {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) <= 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig EqualTo(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must be equal to {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) == 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig NotEqualTo(TProperty other, string error = null)
        {
            if (error == null)
            {
                error = $"Must not be equal to {other}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop.CompareTo(other) != 0, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }

        public TConfig Between(TProperty from, TProperty to, bool isExclusive = true, string error = null)
        {
            if (error == null)
            {
                string exclusiveSetting = string.Empty;
                if (!isExclusive)
                {
                    exclusiveSetting = " including the two endpoints";
                }
                error = $"Must be between {from} and {to}{exclusiveSetting}";
            }
            var validator = new Validator<TProperty>(
                new Func<TProperty, ValidationResult>(
                    (prop) =>
                    {
                        var result = false;
                        if (isExclusive)
                        {
                            result = prop.CompareTo(from) > 0 && prop.CompareTo(to) < 0;
                        }
                        else
                        {
                            result = prop.CompareTo(from) >= 0 && prop.CompareTo(to) <= 0;
                        }
                        return new ValidationResult(result, error);
                    }));
            Validators.Add(validator);
            return this as TConfig;
        }
    }
}
