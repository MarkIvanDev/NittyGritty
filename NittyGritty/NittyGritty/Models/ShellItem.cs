using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ShellItem : ObservableObject
    {
        public ShellItem(ShellItemType type, object content, string key, object parameter, object tag)
        {
            Type = type;
            Content = content;
            Key = key;
            Parameter = parameter;
            Tag = tag;
            Children = new List<ShellItem>();
        }

        public ShellItemType Type { get; }

        public object Content { get; }

        public string Key { get; }

        public object Parameter { get; }

        public object Tag { get; }

        public IList<ShellItem> Children { get; }
    }
}
