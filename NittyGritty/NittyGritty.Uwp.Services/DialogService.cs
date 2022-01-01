using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        {
            return await Show<object>(key, null, null);
        }

        public async Task<bool> Show<T>(string key, T parameter)
        {
            return await Show(key, parameter, null);
        }

        public async Task ShowMessage(string message, string title)
        {
            await ShowMessage(message, title, null);
        }

        public async Task ShowMessage(string message, string title, string buttonText)
        {
            var dialog = CreateDialog(message, title, buttonText);
            await dialog.ShowAsync();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            var dialog = CreateDialog(
                message,
                title,
                buttonConfirmText,
                buttonCancelText);

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        public async Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened)
        {
            var instance = Activator.CreateInstance(GetValue(key));
            if (instance is ContentDialog dialog)
            {
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
            string buttonConfirmText = "OK",
            string buttonCancelText = null)
        {
            var dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                PrimaryButtonText = buttonConfirmText,
                DefaultButton = ContentDialogButton.Primary
            };

            if (!string.IsNullOrEmpty(buttonCancelText))
            {
                dialog.SecondaryButtonText = buttonCancelText;
            }

            return dialog;
        }
    }
}
