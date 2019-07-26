using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class DeviceActivationHandler : ActivationHandler<DeviceActivatedEventArgs>
    {
        public DeviceActivationHandler() : base(ActivationStrategy.Unknown)
        {

        }
    }
}
