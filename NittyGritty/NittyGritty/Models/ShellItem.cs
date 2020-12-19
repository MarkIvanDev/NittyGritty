using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Models
{
    public class ShellItem : ObservableObject
    {
        private ShellItemType _type;

        public ShellItemType Type
        {
            get { return _type; }
            set { Set(ref _type, value); }
        }

        private object _content;

        public object Content
        {
            get { return _content; }
            set { Set(ref _content, value); }
        }

        private string _key;

        public string Key
        {
            get { return _key; }
            set { Set(ref _key, value); }
        }

        private object _parameter;

        public object Parameter
        {
            get { return _parameter; }
            set { Set(ref _parameter, value); }
        }

        private object _tag;

        public object Tag
        {
            get { return _tag; }
            set { Set(ref _tag, value); }
        }

        private IList<ShellItem> _children;

        public IList<ShellItem> Children
        {
            get { return _children ?? (_children = new Collection<ShellItem>()); }
            set { Set(ref _children, value); }
        }

        public static ShellItem Separator => new ShellItem() { Type = ShellItemType.Separator };

    }
}
