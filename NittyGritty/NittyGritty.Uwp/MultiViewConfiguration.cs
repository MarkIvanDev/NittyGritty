using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp
{
    public class MultiViewConfiguration<T>
    {
        public MultiViewConfiguration(Type view, Predicate<T> createsNewView = null)
        {
            if(view != null)
            {
                ViewSelector = (payload) => view;
            }
            else
            {
                throw new ArgumentNullException(nameof(view));
            }
            CreatesNewView = createsNewView ?? (createsNewView = (payload) => false);
        }

        public MultiViewConfiguration(Func<T, Type> viewSelector, Predicate<T> createsNewView = null)
        {
            ViewSelector = viewSelector ?? throw new ArgumentNullException(nameof(viewSelector));
            CreatesNewView = createsNewView ?? (createsNewView = (payload) => false);
        }

        public Func<T, Type> ViewSelector { get; }

        public Predicate<T> CreatesNewView { get; }

        public async Task Show(T payload, int currentlyShownApplicationViewId, Frame frame)
        {
            if (CreatesNewView(payload))
            {
                var newView = CoreApplication.CreateNewView();
                int newViewId = 0;
                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var newViewFrame = new Frame();
                    newViewFrame.Navigate(ViewSelector(payload), payload);
                    Window.Current.Content = newViewFrame;
                    // You have to activate the window in order to show it later.
                    Window.Current.Activate();

                    newViewId = ApplicationView.GetForCurrentView().Id;
                });

                if (currentlyShownApplicationViewId != 0)
                {
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                        newViewId,
                        ViewSizePreference.Default,
                        currentlyShownApplicationViewId,
                        ViewSizePreference.Default);
                }
                else
                {
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                }
            }
            else
            {
                frame.Navigate(ViewSelector(payload), payload);
            }
        }
    }
}
