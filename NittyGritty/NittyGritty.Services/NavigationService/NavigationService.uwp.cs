using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Services
{
    public partial class NavigationService : INavigationContext<Frame>, IConfigurable<Type>
    {

        #region INavigationContext
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
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanGoBack));
                RaisePropertyChanged(nameof(CanGoForward));
                RaisePropertyChanged(nameof(CurrentKey));
            }
        }
        #endregion

        #region IConfigurable
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
        #endregion

        bool PlatformCanGoBack { get { return Context.CanGoBack; } }

        bool PlatformCanGoForward { get { return Context.CanGoForward; } }

        string PlatformCurrentKey
        {
            get
            {
                lock (_viewsByKey)
                {
                    if (Context.BackStackDepth == 0)
                    {
                        return RootKey;
                    }

                    if (Context.Content == null)
                    {
                        return UnknownKey;
                    }

                    var currentType = Context.Content.GetType();

                    if (_viewsByKey.All(p => p.Value != currentType))
                    {
                        return UnknownKey;
                    }

                    var item = _viewsByKey.FirstOrDefault(
                        i => i.Value == currentType);

                    return item.Key;
                }
            }
        }

        void PlatformGoBack()
        {
            if(PlatformCanGoBack)
            {
                Context.GoBack();
            }
        }

        void PlatformGoForward()
        {
            if(PlatformCanGoForward)
            {
                Context.GoForward();
            }
        }

        void PlatformNavigateTo(string key)
        {
            PlatformNavigateTo(key, null);
        }

        void PlatformNavigateTo(string key, object parameter)
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

        void PlatformClearBackStack()
        {
            Context.BackStack.Clear();
        }

        void PlatformClearForwardStack()
        {
            Context.ForwardStack.Clear();
        }
    }
}
