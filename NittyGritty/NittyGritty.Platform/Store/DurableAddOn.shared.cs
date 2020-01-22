﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class DurableAddOn : AddOn
    {
        public DurableAddOn(string id) : base(id, AddOnType.Durable)
        {

        }

        private uint? _lifetime;

        public uint? Lifetime
        {
            get { return _lifetime; }
            set { Set(ref _lifetime, value); }
        }

    }
}