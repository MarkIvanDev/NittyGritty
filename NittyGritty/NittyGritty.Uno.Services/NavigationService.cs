using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NittyGritty.Uno.Services
{
    public class NavigationService : INavigationService, INavigationContext<Frame>, IConfigurable<Type>, INotifyPropertyChanged
    {
        #region INavigationContext
        private Frame _context;

        public Frame Context
        {
            get
            {
                if (_context is null) _context = Window.Current.Content as Frame;
                if (_context != null) _context.Navigated += OnContextNavigated;
                return _context;
            }
            set
            {
                if (_context != null) _context.Navigated -= OnContextNavigated;
                if (value != null) value.Navigated += OnContextNavigated;
                _context = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanGoBack));
                RaisePropertyChanged(nameof(CanGoForward));
                RaisePropertyChanged(nameof(CurrentKey));
            }
        }

        private void OnContextNavigated(object sender, NavigationEventArgs e)
        {
            RaisePropertyChanged(nameof(CanGoBack));
            RaisePropertyChanged(nameof(CanGoForward));
            RaisePropertyChanged(nameof(CurrentKey));
        }

        #endregion

        #region IConfigurable
        private readonly Dictionary<string, Type> _viewsByKey = new Dictionary<string, Type>();

        public void Configure(string key, Type value)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (value is null) throw new ArgumentNullException(nameof(value));

            lock (_viewsByKey)
            {
                if (_viewsByKey.ContainsKey(key))
                {
                    throw new ArgumentException($"This key is already used: {key}");
                }

                if (_viewsByKey.ContainsValue(value))
                {
                    throw new ArgumentException($"This value is already used: {value.FullName}");
                }

                _viewsByKey.Add(key, value);
            }
        }

        public string GetKeyForValue(Type value)
        {
            lock (_viewsByKey)
            {
                return _viewsByKey.FirstOrDefault(i => i.Value == value).Key ??
                    throw new ArgumentException($"The view '{value.Name}' is unknown by the NavigationService");
            }
        }
        #endregion

        #region INavigationService

        public string CurrentKey
        {
            get
            {
                lock (_viewsByKey)
                {
                    return _viewsByKey.FirstOrDefault(
                        i => i.Value == Context.Content?.GetType()).Key;
                }
            }
        }

        public bool CanGoBack { get { return Context.CanGoBack; } }

        public bool CanGoForward { get { return Context.CanGoForward; } }

        public void GoBack()
        {
            if (CanGoBack)
            {
                Context.GoBack();
            }
        }

        public void GoForward()
        {
            if (CanGoForward)
            {
                Context.GoForward();
            }
        }

        public void NavigateTo(string key)
        {
            NavigateTo(key, null);
        }

        public void NavigateTo(string key, object parameter)
        {
            lock (_viewsByKey)
            {
                if (!_viewsByKey.ContainsKey(key))
                {
                    throw new ArgumentException(
                        $"No such view: {key}. Did you forget to call NavigationService.Configure?",
                        nameof(key));
                }

                Context.Navigate(_viewsByKey[key], parameter);
            }
        }

        public void ClearBackStack()
        {
            Context.BackStack.Clear();
        }

        public void ClearForwardStack()
        {
            Context.ForwardStack.Clear();
        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
