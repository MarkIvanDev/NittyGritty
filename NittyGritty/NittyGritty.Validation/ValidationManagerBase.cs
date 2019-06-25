using NittyGritty.Validation.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation
{
    public abstract class ValidationManagerBase<T> where T : class, INotifyPropertyChanged
    {
        public abstract void Start();

        public abstract void Stop();

        public abstract Task Validate();

        public BoolPropertyConfiguration ConfigureProperty(Func<T, bool> property)
        {
            return new BoolPropertyConfiguration();
        }
    }
}
