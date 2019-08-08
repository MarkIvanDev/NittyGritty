using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ShellItem : ObservableObject
    {
        public ShellItem(string key, object parameter, ShellItemType type, object content)
        {
            Key = key;
            Parameter = parameter;
            Type = type;
            Content = content;
        }

        public string Key { get; }

        public object Parameter { get; }

        public ShellItemType Type { get; }

        public object Content { get; }
    }
}
