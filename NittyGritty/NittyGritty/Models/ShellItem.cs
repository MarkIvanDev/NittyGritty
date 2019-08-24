using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ShellItem : ObservableObject
    {
        public ShellItem(ShellItemType type, object content, string key, object parameter, string tag)
        {
            Key = key;
            Parameter = parameter;
            Tag = tag;
            Type = type;
            Content = content;
        }

        public ShellItemType Type { get; }

        public object Content { get; }

        public string Key { get; }

        public object Parameter { get; }

        public string Tag { get; }

    }
}
