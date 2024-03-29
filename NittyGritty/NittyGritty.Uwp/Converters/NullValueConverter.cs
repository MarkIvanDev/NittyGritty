﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uwp.Converters
{
    public class NullValueConverter : IValueConverter
    {
        public object Value { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value ?? Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
