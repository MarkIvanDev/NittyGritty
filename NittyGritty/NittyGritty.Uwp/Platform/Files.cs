using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace NittyGritty.Uwp.Platform
{
    public static class Files
    {
        public static async Task  RequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }


    }
}
