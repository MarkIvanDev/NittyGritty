using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class ConsumableAddOn : AddOn
    {
        public ConsumableAddOn(string id) : base(id, AddOnType.Consumable)
        {

        }

        private uint _quantity;

        public uint Quantity
        {
            get { return _quantity; }
            set { Set(ref _quantity, value); }
        }

        private uint _balance;

        public uint Balance
        {
            get { return _balance; }
            set { Set(ref _balance, value); }
        }

    }
}
