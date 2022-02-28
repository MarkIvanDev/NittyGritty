using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Utilities
{
    public static class CodeHelper
    {

        public static T InvokeOrDefault<T>(Func<T> func, T defaultValue = default)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static async Task<T> InvokeOrDefault<T>(Func<Task<T>> func, T defaultValue = default)
        {
            try
            {
                return await func();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

    }
}
