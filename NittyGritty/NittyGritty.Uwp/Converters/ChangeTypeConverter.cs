﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uwp.Converters
{
    public class ChangeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType.IsConstructedGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            if (value == null && targetType.GetTypeInfo().IsValueType)
                return Activator.CreateInstance(targetType);

            if (targetType.IsInstanceOfType(value))
                return value;

            return System.Convert.ChangeType(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType, parameter, language);
        }
    }
}
