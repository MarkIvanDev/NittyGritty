using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public partial class FileService : IFileService
    {
        public async Task RequestAccess()
        {
            await PlatformRequestAccess();
        }
    }
}
