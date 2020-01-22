using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Events;

namespace NittyGritty.Platform.Store
{
    public interface IInterstitialAd
    {
        event EventHandler AdReady;

        event EventHandler<AdErrorEventArgs> ErrorOccurred;

        event EventHandler Completed;

        event EventHandler Cancelled;

        void Request();

        void Show();

        void Close();
    }

}
