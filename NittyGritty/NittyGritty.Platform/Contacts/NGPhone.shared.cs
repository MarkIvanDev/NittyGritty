using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGPhone : ObservableObject
    {

        private string _number;

        public string Number
        {
            get { return _number; }
            set { Set(ref _number, value); }
        }

        private NGPhoneKind _kind;

        public NGPhoneKind Kind
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

    }
}