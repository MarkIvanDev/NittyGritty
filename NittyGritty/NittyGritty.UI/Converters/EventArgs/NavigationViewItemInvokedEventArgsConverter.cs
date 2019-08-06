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
                        return new ShellItem()
                        {
                            Type = ShellItemType.Settings,
                            Key = navigationView.GetValue(NavigationViewExtensions.SettingsKeyProperty).ToString(),
                            Parameter = navigationView.GetValue(NavigationViewExtensions.SettingsParameterProperty)
                        };
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
                        if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs", "InvokedItemContainer"))
                        {
                            return new ShellItem()
                            {
                                Type = ShellItemType.Item,
                                Key = args.InvokedItemContainer.GetValue(NavigationViewExtensions.KeyProperty).ToString(),
                                Parameter = args.InvokedItemContainer.GetValue(NavigationViewExtensions.ParameterProperty)
                            };
                        }
                        else
                        {
                            var item = navigationView.MenuItems
                                .OfType<NavigationViewItem>()
                                    .FirstOrDefault(menuItem => (string)menuItem.Content == (string)args.InvokedItem);
                            return new ShellItem()
                            {
                                Type = ShellItemType.Item,
                                Key = item?.GetValue(NavigationViewExtensions.KeyProperty).ToString(),
                                Parameter = item?.GetValue(NavigationViewExtensions.ParameterProperty)
                            };
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
