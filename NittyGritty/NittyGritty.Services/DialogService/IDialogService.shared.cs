using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);

        Task ShowMessage(string message, string title, string buttonText);

        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText);

        Task<bool> Show(string key);

        Task<bool> Show<T>(string key, T parameter);

        Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened);

        void HideAll();
    }
}
