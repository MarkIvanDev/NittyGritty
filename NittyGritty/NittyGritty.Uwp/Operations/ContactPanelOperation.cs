using NittyGritty.Uwp.Extensions;
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

        public virtual async Task<object> GetPayload(ContactPanelActivatedEventArgs args)
        {
            var contact = await Contacts.TryGetContact(args.Contact.Id);
            contact.RemoteId = await Contacts.TryGetRemoteId(contact);
            return contact?.ToNGContact();
        }
    }
}
