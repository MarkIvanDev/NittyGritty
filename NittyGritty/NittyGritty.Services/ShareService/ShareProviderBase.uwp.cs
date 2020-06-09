using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.UI;

namespace NittyGritty.Services
{
    public abstract class ShareProviderBase
    {
        public ShareProviderBase(string title, RandomAccessStreamReference icon, Color backgroundColor, IList<string> supportedFormats)
        {
            Title = title;
            Icon = icon;
            BackgroundColor = backgroundColor;
            SupportedFormats = new ReadOnlyCollection<string>(supportedFormats);
        }

        public string Title { get; }

        public RandomAccessStreamReference Icon { get; }

        public Color BackgroundColor { get; }

        public ReadOnlyCollection<string> SupportedFormats { get; }

        public abstract void OnShare(ShareProviderOperation operation);

        public ShareProvider GetShareProvider()
        {
            return new ShareProvider(Title, Icon, BackgroundColor, OnShare);
        }
    }
}
