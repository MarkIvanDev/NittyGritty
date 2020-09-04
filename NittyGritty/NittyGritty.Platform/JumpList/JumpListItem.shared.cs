using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.JumpList
{
    public class JumpListItem : ObservableObject
    {

        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set { Set(ref _groupName, value); }
        }

        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _arguments;

        public string Arguments
        {
            get { return _arguments; }
            set { Set(ref _arguments, value); }
        }

        private Uri _logo;

        public Uri Logo
        {
            get { return _logo; }
            set { Set(ref _logo, value); }
        }

    }
}
