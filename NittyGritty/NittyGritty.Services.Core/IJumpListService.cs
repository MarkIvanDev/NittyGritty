using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.JumpList;

namespace NittyGritty.Services.Core
{
    public interface IJumpListService
    {
        Task Initialize(JumpListSystemManagedGroup systemManagedGroup);

        void Configure(JumpListItem item);

        Task Clear();
    }
}
