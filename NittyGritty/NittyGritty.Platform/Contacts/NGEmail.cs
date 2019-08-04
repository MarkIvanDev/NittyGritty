using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGEmail : ObservableObject
    {

        private NGEmailKind _kind;

        public NGEmailKind Kind
        {
            get { return _kind; }
            set { Set(ref _kind, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { Set(ref _address, value); }
        }

    }
}