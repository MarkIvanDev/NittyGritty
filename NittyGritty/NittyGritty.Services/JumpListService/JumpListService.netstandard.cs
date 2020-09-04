using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.JumpList;

namespace NittyGritty.Services
{
    public partial class JumpListService
    {
        void PlatformConfigure(JumpListItem item)
            => throw new NotImplementedException();

        Task PlatformInitialize(JumpListSystemManagedGroup systemManagedGroup)
            => throw new NotImplementedException();

        Task PlatformClear()
            => throw new NotImplementedException();
    }
}
