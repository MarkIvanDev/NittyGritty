using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ColorInfo : ObservableObject, IEquatable<ColorInfo>
    {
        public ColorInfo() : this(0, 0, 0, 0)
        {

        }

        public ColorInfo(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        private byte _a;

        public byte A
        {
            get { return _a; }
            set { Set(ref _a, value); }
        }

        private byte _r;

        public byte R
        {
            get { return _r; }
            set { Set(ref _r, value); }
        }

        private byte _g;

        public byte G
        {
            get { return _g; }
            set { Set(ref _g, value); }
        }

        private byte _b;

        public byte B
        {
            get { return _b; }
            set { Set(ref _b, value); }
        }

        public string ToHex()
        {
            var hexA = A.ToString("X2");
            var hexR = R.ToString("X2");
            var hexG = G.ToString("X2");
            var hexB = B.ToString("X2");
            return string.Concat("#", hexA, hexR, hexG, hexB);
        }

        public bool Equals(ColorInfo other)
        {
            var result = other != null &&
                   A == other.A &&
                   R == other.R &&
                   G == other.G &&
                   B == other.B;
            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = -1749689076;
            hashCode = hashCode * -1521134295 + A.GetHashCode();
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            return hashCode;
        }
    }
}
