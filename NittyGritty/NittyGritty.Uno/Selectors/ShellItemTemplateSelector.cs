using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uno.Selectors
{
    public class ShellItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ItemTemplate { get; set; }

        public DataTemplate SeparatorTemplate { get; set; }

        public DataTemplate HeaderTemplate { get; set; }

        public DataTemplate SettingsTemplate { get; set; }

        public DataTemplate OtherTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is ShellItem shellItem)
            {
                switch (shellItem.Type)
                {
                    case ShellItemType.Item:
                        return ItemTemplate;
                    case ShellItemType.Separator:
                        return SeparatorTemplate;
                    case ShellItemType.Header:
                        return HeaderTemplate;
                    case ShellItemType.Settings:
                        return SettingsTemplate;
                }
            }
            return OtherTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
