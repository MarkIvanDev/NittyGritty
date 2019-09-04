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
            if(view != null)
            {
                ViewSelector = (payload) => view;
            }
            else
            {
                throw new ArgumentNullException(nameof(view));
            }
        }

        public SingleViewConfiguration(string key, Func<T, Type> viewSelector)
        {
            Key = key;
            ViewSelector = viewSelector ?? throw new ArgumentNullException(nameof(viewSelector));
        }

        public string Key { get; }

        public Func<T, Type> ViewSelector { get; }

        public void Show(T payload, Frame frame)
        {
            frame.Navigate(ViewSelector(payload), payload);
        }
    }
}
