using NittyGritty.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uwp.Services
{
    public class DialogService : IDialogService
    {
        
        private readonly Dictionary<string, Type> _dialogsByKey = new Dictionary<string, Type>();

        public void Configure(string key, Type value)
        {
            lock (_dialogsByKey)
            {
                if (_dialogsByKey.ContainsKey(key))
                {
                    throw new ArgumentException($"This key is already used: {key}");
                }

                if (_dialogsByKey.Any(p => p.Value == value))
                {
                    throw new ArgumentException(
                        "This dialog is already configured with key " + _dialogsByKey.First(p => p.Value == value).Key);
                }

                _dialogsByKey.Add(
                    key,
                    value);
            }
        }

        public string GetKeyForValue(Type value)
        {
            lock (_dialogsByKey)
            {
                if (_dialogsByKey.ContainsValue(value))
                {
                    return _dialogsByKey.FirstOrDefault(p => p.Value == value).Key;
                }
                else
                {
                    throw new ArgumentException($"The dialog '{value.Name}' is unknown by the DialogService");
                }
            }
        }

        public void HideAll()
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
            return await Show<object>(key, null);
        }

        public async Task<bool> Show<T>(string key, T parameter)
        {
            return await Show<T>(key, parameter, null);
        }

        public async Task<bool> Show<T>(string key, T parameter, Func<T, Task> onOpened)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty or whitespace", nameof(key));
            }

            object uObject = null;
            lock (_dialogsByKey)
            {
                if (!_dialogsByKey.ContainsKey(key))
                {
                    throw new ArgumentException($"No such key: {key}. Did you forget to call DialogService.Configure?");
                }
                uObject = Activator.CreateInstance(_dialogsByKey[key]);
            }
            if (uObject is ContentDialog dialog)
            {
                if(onOpened != null)
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

        public async Task ShowMessage(string message, string title)
        {
            var dialog = CreateDialog(message, title);
            await dialog.ShowAsync();
        }

        public async Task ShowMessage(string message, string title, string buttonText)
        {
            var dialog = CreateDialog(message, title, buttonText);
            await dialog.ShowAsync();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            var result = false;

            var dialog = CreateDialog(
                message,
                title,
                buttonConfirmText,
                buttonCancelText,
                r => result = r);

            await dialog.ShowAsync();
            return result;
        }

        private MessageDialog CreateDialog(
            string message,
            string title,
            string buttonConfirmText = "OK",
            string buttonCancelText = null,
            Action<bool> afterHideInternal = null)
        {
            var dialog = new MessageDialog(message, title);

            dialog.Commands.Add(
                new UICommand(
                    buttonConfirmText,
                    o =>
                    {
                        afterHideInternal?.Invoke(true);
                    }));

            dialog.DefaultCommandIndex = 0;

            if (!string.IsNullOrEmpty(buttonCancelText))
            {
                dialog.Commands.Add(
                    new UICommand(
                        buttonCancelText,
                        o =>
                        {
                            afterHideInternal?.Invoke(false);
                        }));

                dialog.CancelCommandIndex = 1;
            }

            return dialog;
        }

    }
}
