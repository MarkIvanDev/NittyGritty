using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Views
{
    public class App : Application
    {
        public App()
        {
            Current = this;
        }

        public static new App Current { get; private set; }



        protected virtual async Task Initialization()
        {
            await Task.CompletedTask;
        }

        protected virtual async Task Startup()
        {
            await Task.CompletedTask;
        }
    }
}
