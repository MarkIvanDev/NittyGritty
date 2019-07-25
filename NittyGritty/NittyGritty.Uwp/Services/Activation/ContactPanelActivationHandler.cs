using NittyGritty.Models;
using NittyGritty.Utilities;
using NittyGritty.Uwp.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Contacts;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    public class ContactPanelActivationHandler : ActivationHandler<ContactPanelActivatedEventArgs>
    {
        private string currentRemoteId = null;

        public ContactPanelActivationHandler(Color headerColor, Type contactView, ContactPanelOperation contactPanelOperation) : base(ActivationStrategy.Hosted)
        {
            HeaderColor = headerColor;
            ContactView = contactView;
            ContactPanelOperation = contactPanelOperation;
        }

        public Color HeaderColor { get; }

        public Type ContactView { get; }

        public ContactPanelOperation ContactPanelOperation { get; }

        protected override async Task HandleInternal(ContactPanelActivatedEventArgs args)
        {
            args.ContactPanel.LaunchFullAppRequested += OnLaunchFullAppRequested;
            args.ContactPanel.HeaderColor = HeaderColor;
            if (Window.Current.Content is Frame frame)
            {
                currentRemoteId = await ContactPanelOperation.GetRemoteId(args.Contact.Id);
                var payload = await ContactPanelOperation.GetPayload(args);
                frame.Navigate(ContactView, payload);
            }
        }

        private async void OnLaunchFullAppRequested(ContactPanel sender, ContactPanelLaunchFullAppRequestedEventArgs args)
        {
            // callback to relaunch the sample app as a protocol launch with the remoteID for the current
            // contact as a launcher argument.
            args.Handled = true;
            await Window.Current.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                async () =>
                {
                    var options = new LauncherOptions
                    {
                        TargetApplicationPackageFamilyName = Package.Current.Id.FamilyName
                    };

                    await Launcher.LaunchUriAsync(
                        ProtocolUtilities.Create(ContactPanelOperation.Scheme, ContactPanelOperation.Path, new QueryString() { { "id", currentRemoteId } }),
                        options);
                    sender.ClosePanel();
                });
        }
    }
}
