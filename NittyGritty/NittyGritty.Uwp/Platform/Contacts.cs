using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.System;

namespace NittyGritty.Uwp.Platform
{
    public static class Contacts
    {
        public static async Task RequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-contacts"));
        }

        public static async Task<Contact> TryGetContact(string id)
        {
            try
            {
                var store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
                return await store.GetContactAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> TryGetRemoteId(Contact contact)
        {
            try
            {
                var store = await ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
                var contactAnnotations = await store.FindAnnotationsForContactAsync(contact);

                if (contactAnnotations.Count >= 0)
                {
                    return contactAnnotations[0].RemoteId;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
