using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    internal class KeyViewConfiguration
    {
        public KeyViewConfiguration(string key, Type view, Predicate<QueryString> createsNewView)
        {
            Key = key;
            View = view;
            CreatesNewView = createsNewView ?? (createsNewView = (q) => false);
        }

        public KeyViewConfiguration(string key, Type view) : this(key, view, (uri) => false)
        {

        }

        public string Key { get; }

        public Type View { get; }

        public Predicate<QueryString> CreatesNewView { get; }

        public async Task Run(QueryString parameters, object payload, int currentlyShownApplicationViewId, Frame frame)
        {
            if (CreatesNewView(parameters))
            {
                var newView = CoreApplication.CreateNewView();
                int newViewId = 0;
                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var newViewFrame = new Frame();
                    newViewFrame.Navigate(View, payload);
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
                frame.Navigate(View, payload);
            }
        }
    }
}
