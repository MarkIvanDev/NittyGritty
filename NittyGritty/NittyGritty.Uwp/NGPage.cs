using NittyGritty.ViewModels;
using NittyGritty.Platform.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.IO;
using Windows.Storage;
using NittyGritty.Data;

namespace NittyGritty.Uwp
{
    public class NGPage : Page
    {
        private IStateManager PageViewModel
        {
            get { return this.DataContext as IStateManager; }
        }

        private string _pageKey;
        private SystemNavigationManager currentView;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this._pageKey = "Page-" + this.Frame.BackStackDepth;

            if (e.NavigationMode == NavigationMode.New)
            {
                // Clear existing state for forward navigation when adding a new page to the
                // navigation stack
                var nextPageKey = this._pageKey;
                int nextPageIndex = this.Frame.BackStackDepth;
                var cachePath = Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, nextPageKey);
                while (File.Exists(cachePath))
                {
                    File.Delete(cachePath);
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                    cachePath = Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, nextPageKey);
                }

                // Pass the navigation parameter to the new page
                PageViewModel?.LoadState(e.Parameter, null);
            }
            else
            {
                // Pass the navigation parameter and preserved page state to the page, using
                // the same strategy for loading suspended state and recreating pages discarded
                // from cache
                PageViewModel?.LoadState(e.Parameter, CacheManager.LoadCache(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, this._pageKey)));
            }

            currentView = SystemNavigationManager.GetForCurrentView();
            currentView.BackRequested += SystemNavigationManager_BackRequested;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var pageState = new Dictionary<string, object>();
            PageViewModel?.SaveState(pageState);
            CacheManager.SaveCache(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, this._pageKey), pageState);

            currentView.BackRequested -= SystemNavigationManager_BackRequested;
            currentView = null;
        }
    }
}
