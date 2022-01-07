using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml;

namespace NittyGritty.Uno.Converters
{
    [ContentProperty(Name = nameof(Dictionary))]
    public abstract class KeyValueConverter<T> : IValueConverter
    {
        public KeyValueConverter()
        {
            Dictionary = new Dictionary<string, T>();
            Default = default;
        }

        public Dictionary<string, T> Dictionary { get; set; }

        public T Default { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Dictionary.TryGetValue(value?.ToString(), out var result) ?
                result : default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
