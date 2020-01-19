using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class UnmanagedConsumableAddOn : AddOn
    {
        public UnmanagedConsumableAddOn(string id) : base(id, AddOnType.UnmanagedConsumable)
        {
        }

        private uint _balance;

        public uint Balance
        {
            get { return _balance; }
            set { Set(ref _balance, value); }
        }

    }
}
