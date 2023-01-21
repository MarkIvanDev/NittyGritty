using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Theme;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uwp.Services
{
    public class DialogService : ConfigurableService<Type>, IDialogService
    {

        public  void HideAll()
        {
            var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
            foreach (var popup in popups)
            {
                if (popup.Child is ContentDialog dialog)
                {
                    dialog.Hide();
                }
                else
                {
                    popup.IsOpen = false;
                }
            }
        }

        public async Task<bool> Show(string key) 
            => await Show(key, AppTheme.Default);

        public async Task<bool> Show(string key, AppTheme theme) 
            => await Show<object>(key, null, null, theme);

        public async Task<bool> Show<T>(string key, T parameter) 
            => await Show(key, parameter, AppTheme.Default);

        public async Task<bool> Show<T>(string key, T parameter, AppTheme theme) 
            => await Show(key, parameter, null, theme);

        public async Task ShowMessage(string message, string title) 
            => await ShowMessage(message, title, AppTheme.Default);

        public async Task ShowMessage(string message, string title, AppTheme theme) 
            => await ShowMessage(message, title, null, theme);

        public async Task ShowMessage(string message, string title, string buttonText) 
            => await ShowMessage(message, title, buttonText, AppTheme.Default);

        public async Task ShowMessage(string message, string title, string buttonText, AppTheme theme) 
            => await CreateDialog(message, title, buttonText, theme: theme).ShowAsync();

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText) 
            => await ShowMessage(message, title, buttonConfirmText, buttonCancelText, AppTheme.Default);

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, AppTheme theme)
        {
            var dialog = CreateDialog(
                message,
                title,
                buttonConfirmText,
                buttonCancelText,
                theme);

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        public async Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened) 
            => await Show(key, parameter, onOpened, AppTheme.Default);

        public async Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened, AppTheme theme)
        {
            var instance = Activator.CreateInstance(GetValue(key));
            if (instance is ContentDialog dialog)
            {
                dialog.RequestedTheme = (ElementTheme)theme;
                if (onOpened != null)
                {
                    dialog.Opened += async (sender, e) =>
                    {
                        await onOpened.Invoke(parameter);
                    };
                }
                dialog.DataContext = parameter;
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    return true;
                }
            }
            return false;
        }

        private ContentDialog CreateDialog(
            string message,
            string title,
            string buttonConfirmText = null,
            string buttonCancelText = null,
            AppTheme theme = AppTheme.Default)
        {
            var dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                PrimaryButtonText = !string.IsNullOrEmpty(buttonConfirmText) ? buttonConfirmText : "OK",
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = (ElementTheme)theme,
            };

            if (!string.IsNullOrEmpty(buttonCancelText))
            {
                dialog.SecondaryButtonText = buttonCancelText;
            }

            return dialog;
        }
    }
}
