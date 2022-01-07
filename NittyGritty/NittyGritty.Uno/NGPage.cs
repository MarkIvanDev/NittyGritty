﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NittyGritty.Data;
using NittyGritty.ViewModels;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NittyGritty.Uno
{
    public partial class NGPage : Page
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

            currentView = SystemNavigationManager.GetForCurrentView();
            currentView.BackRequested += OnBackRequested;

#if WINDOWS_UWP || __WASM__
            // Toggle the visibility of back button based on if the frame can navigate back.
            // Setting it to visible has the follow effect on the platform:
            // - uwp: add a `<-` back button on the title bar
            // - wasm: add a dummy entry in the browser back stack
            currentView.AppViewBackButtonVisibility = Frame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
#endif


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
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            var pageState = new Dictionary<string, object>();
            PageViewModel?.SaveState(pageState);
            CacheManager.SaveCache(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, this._pageKey), pageState);

            currentView.BackRequested -= OnBackRequested;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
#if NETFX_CORE || __ANDROID__ || __WASM__
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                e.Handled = true;
            }
#endif
        }
    }
}