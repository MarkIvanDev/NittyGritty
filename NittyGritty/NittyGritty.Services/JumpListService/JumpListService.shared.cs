using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.JumpList;

namespace NittyGritty.Services
{
    public partial class JumpListService : IJumpListService
    {
        public async Task Clear()
        {
            await PlatformClear();
        }

        public void Configure(JumpListItem item)
        {
            PlatformConfigure(item);
        }

        public async Task Initialize(JumpListSystemManagedGroup systemManagedGroup)
        {
            await PlatformInitialize(systemManagedGroup);
        }
    }
}
