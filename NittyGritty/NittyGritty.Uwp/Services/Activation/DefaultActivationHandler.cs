using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Services.Activation
{
    public class DefaultActivationHandler : ActivationHandler<object>
    {
        public DefaultActivationHandler(Type defaultView)
        {
            DefaultView = defaultView;
        }

        public Type DefaultView { get; }
    }
}
