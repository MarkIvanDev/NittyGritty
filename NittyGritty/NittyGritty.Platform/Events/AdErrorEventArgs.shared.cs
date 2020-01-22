using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Events
{
    public class AdErrorEventArgs : EventArgs
    {
        public AdErrorEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
