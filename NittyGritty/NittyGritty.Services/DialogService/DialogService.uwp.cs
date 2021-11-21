using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Services.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Services
{
    public partial class DialogService : IConfigurable<Type>
    {
        #region IConfigurable
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
        #endregion

        void PlatformHideAll()
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

        async Task<bool> PlatformShow<T>(string key, T parameter, Func<T, Task> onOpened)
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

        async Task PlatformShowMessage(string message, string title, string buttonText)
        {
            var dialog = CreateDialog(message, title, buttonText);
            await dialog.ShowAsync();
        }

        async Task<bool> PlatformShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
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
