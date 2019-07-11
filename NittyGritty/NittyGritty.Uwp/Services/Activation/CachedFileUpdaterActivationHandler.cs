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
        private CachedFileUpdaterUI cachedFileUpdaterUI;
        private FileUpdateRequest fileUpdateRequest;
        private FileUpdateRequestDeferral fileUpdateRequestDeferral;

        public CachedFileUpdaterActivationHandler()
        {
            Strategy = ActivationStrategy.Other;
        }

        public override async Task HandleAsync(CachedFileUpdaterActivatedEventArgs args)
        {
            cachedFileUpdaterUI = args.CachedFileUpdaterUI;
            
            if(cachedFileUpdaterUI.UIStatus == UIStatus.Visible)
            {
                // Normal Activation
                fileUpdateRequest = cachedFileUpdaterUI.UpdateRequest;
                fileUpdateRequestDeferral = cachedFileUpdaterUI.GetDeferral();
            }
            else
            {
                // Background
                cachedFileUpdaterUI.FileUpdateRequested += CachedFileUpdaterUI_FileUpdateRequested;
                cachedFileUpdaterUI.UIRequested += CachedFileUpdaterUI_UIRequested;
            }
        }

        private void CachedFileUpdaterUI_FileUpdateRequested(CachedFileUpdaterUI sender, FileUpdateRequestedEventArgs args)
        {
            switch (cachedFileUpdaterUI.UIStatus)
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

        private async void CachedFileUpdaterUI_UIRequested(CachedFileUpdaterUI sender, object args)
        {
            // The event handler may be invoked on a background thread, so use the Dispatcher to run the UI-related code on the UI thread.
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                
            });
        }

        public override bool CanHandle(CachedFileUpdaterActivatedEventArgs args)
        {
            if(args.CachedFileUpdaterUI.UIStatus == UIStatus.Visible)
            {
                Strategy = ActivationStrategy.Picker;
            }
            else
            {
                Strategy = ActivationStrategy.Background;
            }
            return base.CanHandle(args);
        }
    }
}
