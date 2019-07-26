using NittyGritty.Models;
using NittyGritty.Platform;
using NittyGritty.Platform.Contacts;
using NittyGritty.Uwp.Extensions;
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

        public static async Task<ContactList> FindOrRegisterContactList(string name)
        {
            try
            {
                var store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
                var lists = await store.FindContactListsAsync();

                var contactList = lists.FirstOrDefault(l => l.DisplayName == name);
                if (contactList == null)
                {
                    contactList = await store.CreateContactListAsync(name);
                }

                return contactList;
            }
            catch
            {
                return null;
            }
        }

        private static async Task<ContactAnnotationList> FindOrRegisterAnnotationList()
        {
            var store = await ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
            var lists = await store.FindAnnotationListsAsync();

            var list = lists.FirstOrDefault();
            if (list == null)
            {
                list = await store.CreateAnnotationListAsync();
            }

            return list;
        }

        public static async Task<Contact> CreateContact(string providerId, string listName, NGContact contact)
        {
            try
            {
                var winContact = contact.ToContact();
                var contactList = await FindOrRegisterContactList(listName);
                await contactList.SaveContactAsync(winContact);

                // Annotate this contact with a remote ID, which you can then retrieve when the Contact Panel is activated.
                var contactAnnotation = new ContactAnnotation
                {
                    ContactId = winContact.Id,
                    RemoteId = winContact.RemoteId,
                    SupportedOperations = ContactAnnotationOperations.ContactProfile
                };

                // Annotate that this contact can load this app's Contact Panel.
                var infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
                contactAnnotation.ProviderProperties.Add(providerId, infos[0].AppInfo.AppUserModelId);

                var annotationList = await FindOrRegisterAnnotationList();
                await annotationList.TrySaveAnnotationAsync(contactAnnotation);

                return winContact;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task PinToTaskbar(Contact contact)
        {
            // Get the PinnedContactManager for the current user.
            var pinnedContactManager = PinnedContactManager.GetDefault();

            // Check whether pinning to the taskbar is supported.
            if (!pinnedContactManager.IsPinSurfaceSupported(PinnedContactSurface.Taskbar))
            {
                // If not, then there is nothing for this program to do.
                return;
            }

            if(pinnedContactManager.IsContactPinned(contact, PinnedContactSurface.Taskbar))
            {
                // Contact is already pinned
                return;
            }

            // Pin the contact to the taskbar.
            if (!await pinnedContactManager.RequestPinContactAsync(contact, PinnedContactSurface.Taskbar))
            {
                // Contact was not pinned.
                return;
            }
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

        public static async Task<string> TryGetRemoteId(string id)
        {
            try
            {
                var store = await ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
                var contact = await Contacts.TryGetContact(id);
                var contactAnnotations = await store.FindAnnotationsForContactAsync(contact);

                if (contactAnnotations.Count >= 0)
                {
                    return contactAnnotations[0].RemoteId;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
