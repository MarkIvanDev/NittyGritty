using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ProgressInfo : ObservableObject
    {
        public ProgressInfo()
        {
            Stop();
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            private set { Set(ref _isRunning, value); }
        }

        private double? _total;

        public double? Total
        {
            get { return _total; }
            private set { Set(ref _total, value); }
        }

        private double _value;

        public double Value
        {
            get { return _value; }
            private set { Set(ref _value, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            private set { Set(ref _name, value); }
        }

        public void Update(double? total, double value, string name)
        {
            Total = total;
            Value = value;
            Name = name;
        }

        public void Start()
        {
            IsRunning = true;
            Total = null;
            Value = 0;
            Name = string.Empty;
        }

        public void Stop()
        {
            IsRunning = false;
            Total = 0;
            Value = 0;
            Name = string.Empty;
        }
    }
}
