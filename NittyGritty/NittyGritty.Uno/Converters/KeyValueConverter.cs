﻿using System;
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
    public partial class KeyValueConverter : DependencyObject, IValueConverter
    {
        public KeyValueConverter()
        {
            Dictionary = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Dictionary
        {
            get { return (IDictionary<string, object>)GetValue(DictionaryProperty); }
            set { SetValue(DictionaryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dictionary.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DictionaryProperty =
            DependencyProperty.Register("Dictionary", typeof(IDictionary<string, object>), typeof(KeyValueConverter), new PropertyMetadata(null));



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
