using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGContactDate : ObservableObject
    {

        private int? _year;

        public int? Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }

        private uint? _month;

        public uint? Month
        {
            get { return _month; }
            set { Set(ref _month, value); }
        }

        private uint? _day;

        public uint? Day
        {
            get { return _day; }
            set { Set(ref _day, value); }
        }

        private NGContactDateKind _kind;

        public NGContactDateKind Kind
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