using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class SubscriptionAddOn : AddOn
    {
        public SubscriptionAddOn(string id) : base(id, AddOnType.Subscription)
        {

        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { Set(ref _isActive, value); }
        }

        private uint _billingPeriod;

        public uint BillingPeriod
        {
            get { return _billingPeriod; }
            set { Set(ref _billingPeriod, value); }
        }

        private DurationUnit _billingPeriodUnit;

        public DurationUnit BillingPeriodUnit
        {
            get { return _billingPeriodUnit; }
            set { Set(ref _billingPeriodUnit, value); }
        }

        private uint _trialPeriod;

        public uint TrialPeriod
        {
            get { return _trialPeriod; }
            set { Set(ref _trialPeriod, value); }
        }

        private DurationUnit _trialPeriodUnit;

        public DurationUnit TrialPeriodUnit
        {
            get { return _trialPeriodUnit; }
            set { Set(ref _trialPeriodUnit, value); }
        }

    }
}
