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

        public StringPropertyConfiguration<TOwner> Empty(string error = null)
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

        public StringPropertyConfiguration<TOwner> NotEmpty(string error = null)
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

        public StringPropertyConfiguration<TOwner> Whitespace(string error = null)
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

        public StringPropertyConfiguration<TOwner> NotWhitespace(string error = null)
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

        public StringPropertyConfiguration<TOwner> MaxLength(int maxLength, string error = null)
        {
            if (error == null)
            {
                error = $"Must not exceed {maxLength} character(s)";
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

    }
}
