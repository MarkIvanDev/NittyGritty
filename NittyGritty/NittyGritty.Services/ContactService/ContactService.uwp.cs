using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Contacts;
using Windows.ApplicationModel.Contacts;
using Windows.System;

namespace NittyGritty.Services
{
    public partial class ContactService
    {
        async Task PlatformAnnotate(NGContact contact, NGAnnotations annotations)
        {
            ContactAnnotationOperations winAnnotations = (ContactAnnotationOperations)annotations;
            if (winAnnotations == ContactAnnotationOperations.None)
            {
                winAnnotations = ContactAnnotationOperations.ContactProfile;
            }

            // Annotate this contact with a remote ID, which you can then retrieve when the Contact Panel is activated.
            var contactAnnotation = new ContactAnnotation
            {
                ContactId = contact.Id,
                RemoteId = contact.RemoteId,
                SupportedOperations = winAnnotations
            };

            // Annotate that this contact can load this app's Contact Panel or Contact Share.
            var infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            contactAnnotation.ProviderProperties.Add("ContactPanelAppID", infos[0].AppInfo.AppUserModelId);
            if ((winAnnotations & ContactAnnotationOperations.Share) == ContactAnnotationOperations.Share)
            {
                contactAnnotation.ProviderProperties.Add("ContactShareAppID", infos[0].AppInfo.AppUserModelId);
            }

            var annotationList = await FindOrRegisterAnnotationList();
            await annotationList.TrySaveAnnotationAsync(contactAnnotation);
        }

        async Task PlatformCreateContact(NGContact contact, string listName)
        {
            var winContact = contact.ToContact();
            var contactList = await FindOrRegisterContactList(listName);
            await contactList.SaveContactAsync(winContact);
            contact.Id = winContact.Id;
        }

        async Task PlatformDeleteContact(NGContact contact, string listName)
        {
            var winContact = contact.ToContact();
            var contactList = await FindOrRegisterContactList(listName);
            if (contact != null)
            {
                await contactList.DeleteContactAsync(winContact);
            }
        }

        async Task<NGContact> PlatformGetContact(string id)
        {
            var store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
            var winContact = await store.GetContactAsync(id);
            var contact = await winContact.ToNGContact();
            return contact;
        }

        async Task<NGContact> PlatformGetContactUsingRemoteId(string remoteId, string listName)
        {
            var contactList = await FindOrRegisterContactList(listName);
            var winContact = await contactList.GetContactFromRemoteIdAsync(remoteId);
            var contact = await winContact.ToNGContact();
            return contact;
        }

        async Task<string> PlatformGetRemoteId(NGContact contact)
        {
            var winContact = contact.ToContact();
            var store = await ContactManager.RequestAnnotationStoreAsync(ContactAnnotationStoreAccessType.AppAnnotationsReadWrite);
            var contactAnnotations = await store.FindAnnotationsForContactAsync(winContact);
            return contactAnnotations.FirstOrDefault()?.RemoteId ?? string.Empty;
        }

        async Task PlatformPin(NGContact contact)
        {
            var pinnedContactManager = PinnedContactManager.GetDefault();

            // Check whether pinning to the Taskbar is supported.
            if (!pinnedContactManager.IsPinSurfaceSupported(PinnedContactSurface.Taskbar))
            {
                return;
            }

            var winContact = contact.ToContact();
            if (pinnedContactManager.IsContactPinned(winContact, PinnedContactSurface.Taskbar))
            {
                // Contact is already pinned
                return;
            }

            await pinnedContactManager.RequestPinContactAsync(winContact, PinnedContactSurface.Taskbar);
        }

        async Task PlatformRequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-contacts"));
        }

        async Task PlatformUnpin(NGContact contact)
        {
            var pinnedContactManager = PinnedContactManager.GetDefault();
            var winContact = contact.ToContact();
            if (pinnedContactManager.IsContactPinned(winContact, PinnedContactSurface.Taskbar))
            {
                await pinnedContactManager.RequestUnpinContactAsync(winContact, PinnedContactSurface.Taskbar);
            }
        }

        private async Task<ContactList> FindOrRegisterContactList(string name)
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

        private async Task<ContactAnnotationList> FindOrRegisterAnnotationList()
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
    }
}
