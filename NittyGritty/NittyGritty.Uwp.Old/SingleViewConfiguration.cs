using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp
{
    public class SingleViewConfiguration<T>
    {
        public SingleViewConfiguration(string key, Type view)
        {
            Key = key;
            View = view;
        }

        public string Key { get; }

        public Type View { get; }

        public void Show(T payload, Frame frame)
        {
            frame.Navigate(View, payload);
        }
    }
}
