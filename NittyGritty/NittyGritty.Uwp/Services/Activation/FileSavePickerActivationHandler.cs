using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileSavePickerActivationHandler : ActivationHandler<FileSavePickerActivatedEventArgs>
    {
        public FileSavePickerActivationHandler()
        {
            Strategy = ActivationStrategy.Picker;
        }
    }
}
