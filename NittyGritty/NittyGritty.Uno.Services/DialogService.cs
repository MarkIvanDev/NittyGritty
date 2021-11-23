using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uno.Services
{
    public class DialogService : IDialogService
    {
        #region IConfigurable
        private readonly Dictionary<string, Type> _dialogsByKey = new Dictionary<string, Type>();

        public void Configure(string key, Type value)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (value is null) throw new ArgumentNullException(nameof(value));

            lock (_dialogsByKey)
            {
                if (_dialogsByKey.ContainsKey(key))
                {
                    throw new ArgumentException($"This key is already used: {key}");
                }

                if (_dialogsByKey.ContainsValue(value))
                {
                    throw new ArgumentException($"This value is already used: {value.FullName}");
                }

                _dialogsByKey.Add(key, value);
            }
        }

        public string GetKeyForValue(Type value)
        {
            lock (_dialogsByKey)
            {
                return _dialogsByKey.FirstOrDefault(i => i.Value == value).Key ??
                    throw new ArgumentException($"The view '{value.Name}' is unknown by the DialogService");
            }
        }
        #endregion

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
            return await Show<object>(key, null, null);
        }

        public async Task<bool> Show<T>(string key, T parameter)
        {
            return await Show(key, parameter, null);
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
