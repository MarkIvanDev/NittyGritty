using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public partial class DialogService : IDialogService
    {
        private static readonly object _instanceLock = new object();        private static DialogService _default;
        public static DialogService Default        {            get            {                if (_default == null)                {                    lock (_instanceLock)                    {                        if (_default == null)                        {                            _default = new DialogService();                        }                    }                }                return _default;            }        }

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
