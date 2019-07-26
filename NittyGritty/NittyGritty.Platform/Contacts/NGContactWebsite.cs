using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGContactWebsite : ObservableObject
    {

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _raw;

        public string Raw
        {
            get { return _raw; }
            set { Set(ref _raw, value); }
        }

        public Uri GetUri()
        {
            if(Uri.TryCreate(Raw, UriKind.RelativeOrAbsolute, out var uri))
            {
                return uri;
            }
            return null;
        }
    }
}