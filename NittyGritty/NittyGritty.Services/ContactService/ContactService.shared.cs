using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Contacts;

namespace NittyGritty.Services
{
    public partial class ContactService : IContactService
    {
        public async Task Annotate(NGContact contact, NGAnnotations annotations)
        {
            await PlatformAnnotate(contact, annotations);
        }

        public async Task CreateContact(NGContact contact, string listName)
        {
            await PlatformCreateContact(contact, listName);
        }

        public async Task DeleteContact(NGContact contact, string listName)
        {
            await PlatformDeleteContact(contact, listName);
        }

        public async Task<NGContact> GetContact(string id)
        {
            return await PlatformGetContact(id);
        }

        public async Task<NGContact> GetContactUsingRemoteId(string remoteId, string listName)
        {
            return await PlatformGetContactUsingRemoteId(remoteId, listName);
        }

        public async Task<string> GetRemoteId(NGContact contact)
        {
            return await PlatformGetRemoteId(contact);
        }

        public async Task Pin(NGContact contact)
        {
            await PlatformPin(contact);
        }

        public async Task RequestAccess()
        {
            await PlatformRequestAccess();
        }

        public async Task Unpin(NGContact contact)
        {
            await PlatformUnpin(contact);
        }
    }
}
