using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Shortcut
{
    public class ShortcutItem : ObservableObject
    {

        private string _id;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _arguments;

        public string Arguments
        {
            get { return _arguments; }
            set { Set(ref _arguments, value); }
        }

        private Uri _icon;

        public Uri Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }

        private Uri _smallIcon;

        public Uri SmallIcon
        {
            get { return _smallIcon; }
            set { Set(ref _smallIcon, value); }
        }

        private Uri _wideIcon;

        public Uri WideIcon
        {
            get { return _wideIcon; }
            set { Set(ref _wideIcon, value); }
        }

        private Uri _largeIcon;

        public Uri LargeIcon
        {
            get { return _largeIcon; }
            set { Set(ref _largeIcon, value); }
        }

    }
}
