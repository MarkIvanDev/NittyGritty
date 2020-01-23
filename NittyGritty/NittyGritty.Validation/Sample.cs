using NittyGritty.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Validation
{
    public class Sample : ViewModelBase
    {

        private bool _bool;

        public bool Bool
        {
            get { return _bool; }
            set { Set(ref _bool, value); }
        }

        private char _char;

        public char Char
        {
            get { return _char; }
            set { Set(ref _char, value); }
        }

        private string _string;

        public string String
        {
            get { return _string; }
            set { Set(ref _string, value); }
        }

        private byte _byte;

        public byte Byte
        {
            get { return _byte; }
            set { Set(ref _byte, value); }
        }

        private decimal _decimal;

        public decimal Decimal
        {
            get { return _decimal; }
            set { Set(ref _decimal, value); }
        }

        private double _double;

        public double Double
        {
            get { return _double; }
            set { Set(ref _double, value); }
        }

        private float _float;

        public float Float
        {
            get { return _float; }
            set { Set(ref _float, value); }
        }

        private int _int;

        public int Int
        {
            get { return _int; }
            set { Set(ref _int, value); }
        }

        private long _long;

        public long Long
        {
            get { return _long; }
            set { Set(ref _long, value); }
        }

        private sbyte _SByte;

        public sbyte SByte
        {
            get { return _SByte; }
            set { Set(ref _SByte, value); }
        }

        private short _short;

        public short Short
        {
            get { return _short; }
            set { Set(ref _short, value); }
        }

        private uint _uint;

        public uint UInt
        {
            get { return _uint; }
            set { Set(ref _uint, value); }
        }

        private ulong _ulong;

        public ulong ULong
        {
            get { return _ulong; }
            set { Set(ref _ulong, value); }
        }

        private ushort _ushort;

        public ushort UShort
        {
            get { return _ushort; }
            set { Set(ref _ushort, value); }
        }

        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { Set(ref _dateTime, value); }
        }

        public ValidationManager<Sample> ValidationManager { get; private set; }

        public override void LoadState(object parameter, Dictionary<string, object> state)
        {
            ValidationManager = new ValidationManager<Sample>();
            ValidationManager
                .ConfigureProperty(c => c.DateTime);
        }

        public override void SaveState(Dictionary<string, object> state)
        {
            
        }
    }

    public class SampleValidationManager : ValidationManager<Sample>
    {
        public SampleValidationManager()
        {
            
        }
    }

}
