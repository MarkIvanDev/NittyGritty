using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Theme;

namespace NittyGritty.Services.Core
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);

        Task ShowMessage(string message, string title, AppTheme theme);

        Task ShowMessage(string message, string title, string buttonText);

        Task ShowMessage(string message, string title, string buttonText, AppTheme theme);

        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText);

        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, AppTheme theme);

        Task<bool> Show(string key);

        Task<bool> Show(string key, AppTheme theme);

        Task<bool> Show<T>(string key, T parameter);

        Task<bool> Show<T>(string key, T parameter, AppTheme theme);

        Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened);

        Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened, AppTheme theme);

        void HideAll();
    }
}
