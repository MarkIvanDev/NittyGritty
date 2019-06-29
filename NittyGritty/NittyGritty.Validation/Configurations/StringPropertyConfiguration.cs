using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class StringPropertyConfiguration<TOwner> : ComparablePropertyConfiguration<TOwner, string, StringPropertyConfiguration<TOwner>>
        where TOwner : class
    {
        public StringPropertyConfiguration(Func<TOwner, string> propertyFunc) : base(propertyFunc)
        {
        }

        public StringPropertyConfiguration<TOwner> NullOrEmpty(string error = null)
        {
            if (error == null)
            {
                error = $"Must be empty";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(string.IsNullOrEmpty(prop), error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> NotNullOrEmpty(string error = null)
        {
            if (error == null)
            {
                error = $"Must not be empty";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(!string.IsNullOrEmpty(prop), error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> NullOrWhitespace(string error = null)
        {
            if (error == null)
            {
                error = $"Must be whitespace";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(string.IsNullOrWhiteSpace(prop), error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> NotNullOrWhitespace(string error = null)
        {
            if (error == null)
            {
                error = $"Must not be whitespace";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(!string.IsNullOrWhiteSpace(prop), error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> ExactLength(int exactLength, string error = null)
        {
            if (error == null)
            {
                error = $"Must have {exactLength} character(s)";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop != null && prop.Length == exactLength, error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> MaxLength(int maxLength, string error = null)
        {
            if (error == null)
            {
                error = $"Must have a maximum of {maxLength} character(s)";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop != null && prop.Length <= maxLength, error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> MinLength(int minLength, string error = null)
        {
            if (error == null)
            {
                error = $"Must have a minimum of {minLength} character(s)";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop != null && prop.Length >= minLength, error);
                    }));
            Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration<TOwner> BetweenLength(int minLength, int maxLength, string error = null)
        {
            if (error == null)
            {
                error = $"Must have a minimum of {minLength} and maximum of {maxLength} character(s)";
            }
            var validator = new Validator<string>(
                new Func<string, ValidationResult>(
                    (prop) =>
                    {
                        return new ValidationResult(prop != null && prop.Length >= minLength && prop.Length <= maxLength, error);
                    }));
            Validators.Add(validator);
            return this;
        }

    }
}
