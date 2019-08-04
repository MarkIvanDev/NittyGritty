using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGAddress : ObservableObject
    {

        private string _street;

        public string Street
        {
            get { return _street; }
            set { Set(ref _street, value); }
        }

        private string _locality;

        public string Locality
        {
            get { return _locality; }
            set { Set(ref _locality, value); }
        }

        private string _region;

        public string Region
        {
            get { return _region; }
            set { Set(ref _region, value); }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { Set(ref _country, value); }
        }

        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set { Set(ref _postalCode, value); }
        }

        private NGAddressKind _kind;

        public NGAddressKind Kind
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