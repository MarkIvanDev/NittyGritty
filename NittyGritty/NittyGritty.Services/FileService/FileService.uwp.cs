using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace NittyGritty.Services
{
    public partial class FileService
    {
        async Task PlatformRequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }
    }
}
