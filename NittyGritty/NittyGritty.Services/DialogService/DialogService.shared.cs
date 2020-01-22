using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public partial class DialogService : IDialogService
    {
        public void HideAll()
        {
            PlatformHideAll();
        }

        public async Task<bool> Show(string key)
        {
            return await PlatformShow<object>(key, null, null);
        }

        public async Task<bool> Show<T>(string key, T parameter)
        {
            return await PlatformShow(key, parameter, null);
        }

        public async Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened)
        {
            return await PlatformShow(key, parameter, onOpened);
        }

        public async Task ShowMessage(string message, string title)
        {
            await PlatformShowMessage(message, title, null);
        }

        public async Task ShowMessage(string message, string title, string buttonText)
        {
            await PlatformShowMessage(message, title, buttonText);
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            return await PlatformShowMessage(message, title, buttonConfirmText, buttonCancelText);
        }
    }
}
