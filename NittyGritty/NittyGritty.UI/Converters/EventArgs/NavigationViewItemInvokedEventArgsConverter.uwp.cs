using NittyGritty.Models;
using NittyGritty.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace NittyGritty.UI.Converters.EventArgs
{
    public class NavigationViewItemInvokedEventArgsConverter : IValueConverter
    {
        public NavigationViewItemStrategy Strategy { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is NavigationViewItemInvokedEventArgs args && parameter is NavigationView navigationView)
            {
                if (args.IsSettingsInvoked)
                {
                    if (Strategy != NavigationViewItemStrategy.Other)
                    {
                        return new ShellItem(
                            type: ShellItemType.Settings,
                            content: null,
                            key: navigationView.GetValue(NavigationViewExtensions.SettingsKeyProperty).ToString(),
                            parameter: navigationView.GetValue(NavigationViewExtensions.SettingsParameterProperty),
                            tag: null);
                    }
                    else
                    {
                        return args.InvokedItem;
                    }
                }
                else
                {
                    if(Strategy == NavigationViewItemStrategy.Extension)
                    {
                        if (ApiInformation.IsReadOnlyPropertyPresent("Windows.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs", "InvokedItemContainer"))
                        {
                            return new ShellItem(
                                type: ShellItemType.Item,
                                content: args.InvokedItemContainer.Content,
                                key: args.InvokedItemContainer.GetValue(NavigationViewExtensions.KeyProperty).ToString(),
                                parameter: args.InvokedItemContainer.GetValue(NavigationViewExtensions.ParameterProperty),
                                tag: null);
                        }
                        else
                        {
                            var item = navigationView.MenuItems
                                .OfType<NavigationViewItem>()
                                    .FirstOrDefault(menuItem => (string)menuItem.Content == (string)args.InvokedItem);
                            return new ShellItem(
                                type: ShellItemType.Item,
                                content: item?.Content,
                                key: item?.GetValue(NavigationViewExtensions.KeyProperty).ToString(),
                                parameter: item?.GetValue(NavigationViewExtensions.ParameterProperty),
                                tag: null);
                        }
                    }
                    else
                    {
                        return args.InvokedItem;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public enum NavigationViewItemStrategy
    {
        ShellItem = 0,
        Extension = 1,
        Other = 9
    }
}
