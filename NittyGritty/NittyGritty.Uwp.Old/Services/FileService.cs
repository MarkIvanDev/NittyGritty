using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace NittyGritty.Uwp.Services
{
    internal class FileService
    {
        public async Task RequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }
    }
}
