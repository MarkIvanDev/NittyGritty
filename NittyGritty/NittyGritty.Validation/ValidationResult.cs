using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation
{
    public class ValidationResult : ObservableObject
    {
        public ValidationResult(bool isValid, string error)
        {
            IsValid = isValid;
            Error = error;
        }

        public bool IsValid { get; }

        public string Error { get; }
    }
}
