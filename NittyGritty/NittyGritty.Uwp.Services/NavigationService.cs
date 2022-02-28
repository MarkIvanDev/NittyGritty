using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NittyGritty.Uwp.Services
{
    public class NavigationService : ConfigurableService<Type>, INavigationService, INavigationContext<Frame>, INotifyPropertyChanged
    {
        #region INavigationContext
        private Frame _context;

        public Frame Context
        {
            get
            {
                if (_context is null)
                {
                    _context = Window.Current.Content as Frame;
                    if (_context != null) _context.Navigated += OnContextNavigated;
                }
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

        #region INavigationService

        public string CurrentKey
        {
            get
            {
                return GetKeyForValue(Context.Content?.GetType());
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
            Context.Navigate(GetValue(key), parameter);
        }

        public void ClearBackStack()
        {
            Context.BackStack.Clear();
            RaisePropertyChanged(nameof(CanGoBack));
        }

        public void ClearForwardStack()
        {
            Context.ForwardStack.Clear();
            RaisePropertyChanged(nameof(CanGoForward));
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
