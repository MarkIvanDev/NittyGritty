using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NittyGritty.Extensions
{
    public static class StringExtensions
    {

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhitespace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsBetweenLength(this string value, int minLength, int maxLength)
        {
            if(minLength < 0 || maxLength < 0)
            {
                throw new ArgumentException("String length should not be less than 0");
            }

            if(minLength > maxLength)
            {
                throw new ArgumentException("Minimum length should be less than Maximum length");
            }

            if(value.IsNotNullOrEmpty())
            {
                return minLength == 0 ? true : false;
            }
            else
            {
                return value.Length >= minLength && value.Length <= maxLength ? true : false;
            }
        }

        public static bool IsExactLength(this string value, int exactLength)
        {
            return value.IsBetweenLength(exactLength, exactLength);
        }

        public static bool IsMinLength(this string value, int minLength)
        {
            return value.IsBetweenLength(minLength, int.MaxValue);
        }

        public static bool IsMaxLength(this string value, int maxLength)
        {
            return value.IsBetweenLength(0, maxLength);
        }

        public static bool IsRegex(this string value, string regex)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return new Regex(regex, RegexOptions.IgnoreCase).IsMatch(value);
        }
    }
}
