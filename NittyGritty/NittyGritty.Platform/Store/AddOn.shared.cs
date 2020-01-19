using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public abstract class AddOn : ObservableObject
    {
        public AddOn(string id, AddOnType type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get; }

        public AddOnType Type { get; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { Set(ref _price, value); }
        }

        private string _customData;

        public string CustomData
        {
            get { return _customData; }
            set { Set(ref _customData, value); }
        }


        public enum AddOnType
        {
            Unknown = 0,
            Consumable = 1,
            UnmanagedConsumable = 2,
            Durable = 3,
            Subscription = 4
        }
    }
}
