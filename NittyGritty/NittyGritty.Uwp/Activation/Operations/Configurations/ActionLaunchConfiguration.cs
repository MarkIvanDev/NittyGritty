using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class ActionLaunchConfiguration
    {
        public ActionLaunchConfiguration(string action, MultiViewConfiguration<QueryString> view)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Action cannot be null, empty, or whitespace", nameof(action));
            }

            Action = action;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public string Action { get; }

        public MultiViewConfiguration<QueryString> View { get; }
    }
}
