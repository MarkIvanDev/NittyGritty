using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage.Provider;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Services.Activation
{
    internal class CachedFileUpdaterActivationHandler : ActivationHandler<CachedFileUpdaterActivatedEventArgs>
    {
        private FileUpdateRequest fileUpdateRequest;
        private FileUpdateRequestDeferral fileUpdateRequestDeferral;

        public CachedFileUpdaterActivationHandler() : base(ActivationStrategy.Unknown)
        {
            
        }

        protected override async Task HandleInternal(CachedFileUpdaterActivatedEventArgs args)
        {
            if(args.CachedFileUpdaterUI.UIStatus == UIStatus.Visible)
            {
                // Normal Activation
                fileUpdateRequest = args.CachedFileUpdaterUI.UpdateRequest;
                fileUpdateRequestDeferral = args.CachedFileUpdaterUI.GetDeferral();
            }
            else
            {
                // Background
                args.CachedFileUpdaterUI.FileUpdateRequested += OnFileUpdateRequested;
                args.CachedFileUpdaterUI.UIRequested += OnUIRequested;
            }
            await Task.CompletedTask;
        }

        private void OnFileUpdateRequested(CachedFileUpdaterUI sender, FileUpdateRequestedEventArgs args)
        {
            switch (sender.UIStatus)
            {
                case UIStatus.Hidden:
                    fileUpdateRequest = args.Request;
                    fileUpdateRequestDeferral = fileUpdateRequest.GetDeferral();
                    // process cached file
                    fileUpdateRequestDeferral.Complete();
                    break;
                case UIStatus.Visible:
                    // use the class fields

                    fileUpdateRequestDeferral.Complete();
                    break;
                case UIStatus.Unavailable:
                    fileUpdateRequest.Status = FileUpdateStatus.Failed;
                    fileUpdateRequestDeferral.Complete();
                    break;
            }
        }

        private async void OnUIRequested(CachedFileUpdaterUI sender, object args)
        {
            // The event handler may be invoked on a background thread, so use the Dispatcher to run the UI-related code on the UI thread.
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                
            });
        }

    }
}
