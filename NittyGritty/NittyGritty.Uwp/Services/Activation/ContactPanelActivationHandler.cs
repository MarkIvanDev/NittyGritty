using NittyGritty.Models;
using NittyGritty.Utilities;
using NittyGritty.Uwp.Operations;
using NittyGritty.Uwp.Platform;
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
        public ContactPanelActivationHandler(Color headerColor, Type contactView, ContactPanelOperation contactPanelOperation) : base(ActivationStrategy.Hosted)
        {
            HeaderColor = headerColor;
            ContactView = contactView ?? throw new ArgumentNullException(nameof(contactView), "Contact Panel View cannot be null");
            ContactPanelOperation = contactPanelOperation ?? throw new ArgumentNullException(nameof(contactPanelOperation), "ContactPanelOperation cannot be null");
        }

        public Color HeaderColor { get; }

        public Type ContactView { get; }

        public ContactPanelOperation ContactPanelOperation { get; }

        protected override async Task HandleInternal(ContactPanelActivatedEventArgs args)
        {
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

                        await Launcher.LaunchUriAsync(
                            ProtocolUtilities.Create(ContactPanelOperation.Scheme, ContactPanelOperation.Path,
                                new QueryString() { { "id", await Contacts.TryGetRemoteId(args.Contact.Id) } }),
                            options);
                        contactPanel.ClosePanel();
                    });
            };
            
            if (Window.Current.Content is Frame frame)
            {   
                var payload = await ContactPanelOperation.GetPayload(args);
                frame.Navigate(ContactView, payload);
            }
        }

    }
}
