using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public partial class DialogService
    {
        void PlatformHideAll()
            => throw new NotImplementedException();

        Task<bool> PlatformShow<T>(string key, T parameter, Func<T, Task> onOpened)
            => throw new NotImplementedException();

        Task PlatformShowMessage(string message, string title, string buttonText)
            => throw new NotImplementedException();

        Task<bool> PlatformShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
            => throw new NotImplementedException();
    }
}
