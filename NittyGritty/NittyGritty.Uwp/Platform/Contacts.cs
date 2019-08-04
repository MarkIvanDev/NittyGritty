using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.System;

namespace NittyGritty.Uwp.Platform
{
    internal static class Contacts
    {
        public static async Task RequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-contacts"));
        }

        public static async Task<ContactList> FindOrRegisterContactList(string name)
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

        public static async Task<Contact> CreateContact(Contact contact, string listName)
        {
            var contactList = await FindOrRegisterContactList(listName);
            await contactList.SaveContactAsync(contact);

            return contact;
        }

        public static async Task DeleteContact(Contact contact, string listName)
        {
            var contactList = await FindOrRegisterContactList(listName);
            if (contact != null)
            {
                await contactList.DeleteContactAsync(contact);
            }
        }

        public static async Task AnnotateContact(Contact contact, ContactAnnotationOperations annotations)
        {
            if (annotations == ContactAnnotationOperations.None)
            {
                annotations = ContactAnnotationOperations.ContactProfile;
            }

            // Annotate this contact with a remote ID, which you can then retrieve when the Contact Panel is activated.
            var contactAnnotation = new ContactAnnotation
            {
                ContactId = contact.Id,
                RemoteId = contact.RemoteId,
                SupportedOperations = annotations
            };

            // Annotate that this contact can load this app's Contact Panel or Contact Share.
            var infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            contactAnnotation.ProviderProperties.Add("ContactPanelAppID", infos[0].AppInfo.AppUserModelId);
            if ((annotations & ContactAnnotationOperations.Share) == ContactAnnotationOperations.Share)
            {
                contactAnnotation.ProviderProperties.Add("ContactShareAppID", infos[0].AppInfo.AppUserModelId);
            }

            var annotationList = await FindOrRegisterAnnotationList();
            await annotationList.TrySaveAnnotationAsync(contactAnnotation);
        }

        public static async Task PinToTaskbar(Contact contact)
        {
            var pinnedContactManager = PinnedContactManager.GetDefault();

            // Check whether pinning to the Taskbar is supported.
            if (!pinnedContactManager.IsPinSurfaceSupported(PinnedContactSurface.Taskbar))
            {
                return;
            }

            if(pinnedContactManager.IsContactPinned(contact, PinnedContactSurface.Taskbar))
            {
                // Contact is already pinned
                return;
            }

            await pinnedContactManager.RequestPinContactAsync(contact, PinnedContactSurface.Taskbar);
        }

        public static async Task UnpinFromTaskbar(Contact contact)
        {
            var pinnedContactManager = PinnedContactManager.GetDefault();

            if (pinnedContactManager.IsContactPinned(contact, PinnedContactSurface.Taskbar))
            {
                await pinnedContactManager.RequestUnpinContactAsync(contact, PinnedContactSurface.Taskbar);
            }
        }

        public static async Task<Contact> TryGetContact(string id)
        {
            var store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
            var winContact = await store.GetContactAsync(id);
            return winContact;
        }

        public static async Task<Contact> TryGetContactUsingRemoteId(string remoteId, string listName)
        {
            var contactList = await FindOrRegisterContactList(listName);
            var winContact = await contactList.GetContactFromRemoteIdAsync(remoteId);
            return winContact;
        }

        public static async Task<string> TryGetRemoteId(Contact contact)
        {
            var store = await ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
            var contactAnnotations = await store.FindAnnotationsForContactAsync(contact);
            return contactAnnotations.FirstOrDefault()?.RemoteId ?? string.Empty;
        }

    }
}
