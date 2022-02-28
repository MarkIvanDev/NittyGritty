using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NittyGritty.Data;
using NittyGritty.Uno.Extensions;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var frameKey = FrameExtensions.GetKey(this.Frame);
            this._pageKey = $"{frameKey}-Page{this.Frame.BackStackDepth}";

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
                    nextPageKey = $"{frameKey}-Page{nextPageIndex}";
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var pageState = new Dictionary<string, object>();
            PageViewModel?.SaveState(pageState);
            CacheManager.SaveCache(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, this._pageKey), pageState);
        }

    }
}
