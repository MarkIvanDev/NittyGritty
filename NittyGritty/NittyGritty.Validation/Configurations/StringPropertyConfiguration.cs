using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation.Configurations
{
    public class StringPropertyConfiguration : ComparablePropertyConfiguration<StringPropertyConfiguration, string>
    {
        public StringPropertyConfiguration Empty(string error = null)
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
            this.Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration NotEmpty(string error = null)
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
            this.Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration Whitespace(string error = null)
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
            this.Validators.Add(validator);
            return this;
        }

        public StringPropertyConfiguration NotWhitespace(string error = null)
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
            this.Validators.Add(validator);
            return this;
        }

    }
}
