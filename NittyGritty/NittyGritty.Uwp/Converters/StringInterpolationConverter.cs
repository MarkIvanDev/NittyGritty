using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Converters
{
    public class StringInterpolationConverter : MultiConverter<string>
    {
        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(StringInterpolationConverter), new PropertyMetadata(null));

        public override string Convert()
        {
            return string.Format(Format, Bindings.Select(b => b.Value).ToArray());
        }
    }
}
