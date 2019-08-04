using NittyGritty.Models;
using NittyGritty.Uwp.Extensions;
using NittyGritty.Uwp.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation
{
    public class ContactPanelActivationHandler : ActivationHandler<ContactPanelActivatedEventArgs>
    {
        public ContactPanelActivationHandler(Color headerColor, Type contactView, Uri protocolHeader) : base(ActivationStrategy.Hosted)
        {
            HeaderColor = headerColor;
            ContactView = contactView ?? throw new ArgumentNullException(nameof(contactView), "Contact Panel View cannot be null");
            ProtocolHeader = protocolHeader;
        }

        public Color HeaderColor { get; }

        public Type ContactView { get; }

        public Uri ProtocolHeader { get; }

        protected override async Task HandleInternal(ContactPanelActivatedEventArgs args)
        {
            var contact = await Contacts.TryGetContact(args.Contact.Id);
            contact.RemoteId = await Contacts.TryGetRemoteId(contact);
            args.ContactPanel.HeaderColor = HeaderColor;
            args.ContactPanel.LaunchFullAppRequested += async (contactPanel, e) =>
            {
                e.Handled = true;
                await Window.Current.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        var options = new LauncherOptions
                        {
                            TargetApplicationPackageFamilyName = Package.Current.Id.FamilyName
                        };

                        var uri = new UriBuilder(ProtocolHeader);
                        uri.Query = new QueryString() { { "id", contact.RemoteId } }.ToString();
                        await Launcher.LaunchUriAsync(uri.Uri, options);
                        contactPanel.ClosePanel();
                    });
            };
            
            if (Window.Current.Content is Frame frame)
            {
                var ngContact = await contact.ToNGContact();
                frame.Navigate(ContactView, ngContact);
            }
        }

    }
}
