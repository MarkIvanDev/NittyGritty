using NittyGritty.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services
{
    public class NavigationService : INavigationService, IViewConfigurable<Type>, INavigationContext<Frame>
    {
        private readonly Dictionary<string, Type> _viewsByKey = new Dictionary<string, Type>();

        public void Configure(string key, Type value)
        {
            lock (_viewsByKey)
            {
                if (_viewsByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (_viewsByKey.Any(p => p.Value == value))
                {
                    throw new ArgumentException(
                        "This view is already configured with key " + _viewsByKey.First(p => p.Value == value).Key);
                }

                _viewsByKey.Add(
                    key,
                    value);
            }
        }

        public string GetKeyForValue(Type value)
        {
            lock (_viewsByKey)
            {
                if (_viewsByKey.ContainsValue(value))
                {
                    return _viewsByKey.FirstOrDefault(p => p.Value == value).Key;
                }
                else
                {
                    throw new ArgumentException($"The view '{value.Name}' is unknown by the NavigationService");
                }
            }
        }

        public string CurrentKey
        {
            get
            {
                lock (_viewsByKey)
                {
                    if (Context.BackStackDepth == 0)
                    {
                        return ViewConstants.RootKey;
                    }

                    if (Context.Content == null)
                    {
                        return ViewConstants.UnknownKey;
                    }

                    var currentType = Context.Content.GetType();

                    if (_viewsByKey.All(p => p.Value != currentType))
                    {
                        return ViewConstants.UnknownKey;
                    }

                    var item = _viewsByKey.FirstOrDefault(
                        i => i.Value == currentType);

                    return item.Key;
                }
            }
        }

        private Frame _context;

        public Frame Context
        {
            get
            {
                return _context ?? (_context = ((Frame)Window.Current.Content));
            }
            set
            {
                _context = value;
            }
        }

        public void GoBack()
        {
            if (Context.CanGoBack)
            {
                Context.GoBack();
            }
        }

        public void NavigateTo(string key)
        {
            NavigateTo(key, null);
        }

        public virtual void NavigateTo(string key, object parameter)
        {
            lock (_viewsByKey)
            {
                if (!_viewsByKey.ContainsKey(key))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such view: {0}. Did you forget to call NavigationService.Configure?",
                            key),
                        nameof(key));
                }

                Context.Navigate(_viewsByKey[key], parameter);
            }
        }

    }
}
