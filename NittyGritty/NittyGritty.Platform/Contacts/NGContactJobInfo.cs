using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGContactJobInfo : ObservableObject
    {

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _office;

        public string Office
        {
            get { return _office; }
            set { Set(ref _office, value); }
        }

        private string _manager;

        public string Manager
        {
            get { return _manager; }
            set { Set(ref _manager, value); }
        }

        private string _department;

        public string Department
        {
            get { return _department; }
            set { Set(ref _department, value); }
        }

        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set { Set(ref _companyName, value); }
        }

        private string _companyAddress;

        public string CompanyAddress
        {
            get { return _companyAddress; }
            set { Set(ref _companyAddress, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

    }
}