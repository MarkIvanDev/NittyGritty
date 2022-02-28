using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Extensions;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uwp.Converters
{
    public class SwitchConverter : Collection<Case>, IValueConverter
    {
        public Case Default { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var pick = this.FirstOrDefault(c => value == c.Value || (value?.EqualTo(c.Value) ?? false) || (c.Value?.EqualTo(value) ?? false));
            return pick?.Result ?? Default?.Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class Case
    {
        public object Value { get; set; }

        public object Result { get; set; }
    }
}
