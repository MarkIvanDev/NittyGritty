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

        private SubscriptionPeriod _period;

        public SubscriptionPeriod Period
        {
            get { return _period; }
            set { Set(ref _period, value); }
        }

        private SubscriptionTrial _trial;

        public SubscriptionTrial Trial
        {
            get { return _trial; }
            set { Set(ref _trial, value); }
        }

        public enum SubscriptionPeriod
        {
            Monthly = 0,
            Quarterly = 1,
            Semiannually = 2,
            Annually = 3,
            Biennially = 4
        }

        public enum SubscriptionTrial
        {
            None = 0,
            OneWeek = 1,
            OneMonth = 2
        }
    }
}
