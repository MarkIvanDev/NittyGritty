using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class DurableAddOn : AddOn, IActiveAddOn
    {
        public DurableAddOn(string id, uint? durationPeriod = null, DurationUnit? durationUnit = null) : base(id, AddOnType.Durable)
        {
            DurationPeriod = durationPeriod;
            DurationUnit = durationUnit;
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { Set(ref _isActive, value); }
        }

        private uint? _durationPeriod;

        public uint? DurationPeriod
        {
            get { return _durationPeriod; }
            set { Set(ref _durationPeriod, value); }
        }

        private DurationUnit? _durationUnit;

        public DurationUnit? DurationUnit
        {
            get { return _durationUnit; }
            set { Set(ref _durationUnit, value); }
        }

    }
}
