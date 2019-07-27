using NittyGritty.ViewModels;
using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NittyGritty.Uwp.Views
{
    public class NGPage : Page
    {
        private IStateManager PageViewModel
        {
            get { return this.DataContext as IStateManager; }
        }

        private string _pageKey;
        private SystemNavigationManager currentView;

        public NGPage()
        {
            if(!DesignMode.DesignModeEnabled)
            {
                this.LoadState += ViewBase_LoadState;
                this.SaveState += ViewBase_SaveState;
            }
        }


        void ViewBase_SaveState(object sender, SaveStateEventArgs e)
        {
            if (PageViewModel != null)
            {
                PageViewModel.SaveState(e.State);
            }
        }

        void ViewBase_LoadState(object sender, LoadStateEventArgs e)
        {
            if (PageViewModel != null)
            {
                PageViewModel.LoadState(e.Parameter, e.State);
            }
        }

        /// <summary>
        /// Register this event on the current page to populate the page
        /// with content passed during navigation as well as any saved
        /// state provided when recreating a page from a prior session.
        /// </summary>
        public event LoadStateEventHandler LoadState;

        /// <summary>
        /// Register this event on the current page to preserve
        /// state associated with the current page in case the
        /// application is suspended or the page is discarded from
        /// the navigaqtion cache.
        /// </summary>
        public event SaveStateEventHandler SaveState;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            this._pageKey = "Page-" + this.Frame.BackStackDepth;

            if (e.NavigationMode == NavigationMode.New)
            {
                // Clear existing state for forward navigation when adding a new page to the
                // navigation stack
                var nextPageKey = this._pageKey;
                int nextPageIndex = this.Frame.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                }

                // Pass the navigation parameter to the new page
                this.LoadState?.Invoke(this, new LoadStateEventArgs(e.Parameter, null));
            }
            else
            {
                // Pass the navigation parameter and preserved page state to the page, using
                // the same strategy for loading suspended state and recreating pages discarded
                // from cache
                this.LoadState?.Invoke(this, new LoadStateEventArgs(e.Parameter, (Dictionary<string, object>)frameState[this._pageKey]));
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

            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            var pageState = new Dictionary<string, object>();
            this.SaveState?.Invoke(this, new SaveStateEventArgs(pageState));
            frameState[_pageKey] = pageState;

            currentView.BackRequested -= SystemNavigationManager_BackRequested;
        }
    }
}
