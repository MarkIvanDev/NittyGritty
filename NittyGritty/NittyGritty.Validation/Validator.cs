using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation
{
    public class Validator<T>
    {
        private readonly Func<T, ValidationResult> validator;
        private readonly Func<T, Task<ValidationResult>> validatorAsync;

        public Validator(Func<T, ValidationResult> validator)
        {
            this.validator = validator;
        }

        public Validator(Func<T, Task<ValidationResult>> validatorAsync)
        {
            this.validatorAsync = validatorAsync;
        }

        public async Task<ValidationResult> ValidateAsync(T property)
        {
            if(validator != null)
            {
                return validator(property);
            }
            else if(validatorAsync != null)
            {
                return await validatorAsync(property);
            }
            else
            {
                throw new InvalidOperationException("A validator must be supplied");
            }
        }
    }
}
