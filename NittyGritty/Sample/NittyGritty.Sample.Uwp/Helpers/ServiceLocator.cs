using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using NittyGritty.Sample.ViewModels;
using NittyGritty.Services;

namespace NittyGritty.Sample.Uwp.Helpers
{
    public class ServiceLocator : ViewModelLocator
    {
        public ServiceLocator() : base()
        {
            SimpleIoc.Default.Register<IClipboardService, ClipboardService>();
        }
    }
}
