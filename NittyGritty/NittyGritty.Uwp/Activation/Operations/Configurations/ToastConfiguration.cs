using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class ToastConfiguration
    {
        public ToastConfiguration(string action, MultiViewConfiguration<ToastPayload> view)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Action cannot be null, empty, or whitespace", nameof(action));
            }

            Action = action;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public string Action { get; }

        public MultiViewConfiguration<ToastPayload> View { get; }
    }
}
