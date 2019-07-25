using NittyGritty.Uwp.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Operations
{
    public class ContactPanelOperation
    {
        public ContactPanelOperation(string scheme, string path)
        {
            if(string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentException("Scheme cannot be null, empty or whitespace", nameof(scheme));
            }

            Scheme = scheme;
            Path = path ?? string.Empty;
        }

        public string Scheme { get; }

        public string Path { get; }

        public async Task<string> GetRemoteId(string id)
        {
            try
            {
                var contact = await Contacts.TryGetContact(id);
                var remoteId = await Contacts.TryGetRemoteId(contact);
                return remoteId;
            }
            catch (Exception)
            {
                await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new MessageDialog("This app requires access to your contacts in order to show information about this contact.");
                    dialog.Commands.Add(new UICommand(
                        "Allow Access",
                        async (label) => await Contacts.RequestAccess()));
                    dialog.Commands.Add(new UICommand(
                        "Cancel"));

                    dialog.DefaultCommandIndex = 0;
                    dialog.CancelCommandIndex = 1;
                    
                    await dialog.ShowAsync();
                });
                return null;
            }
        }

        public virtual async Task<object> GetPayload(ContactPanelActivatedEventArgs args)
        {
            return await GetRemoteId(args.Contact.Id);
        }
    }
}
