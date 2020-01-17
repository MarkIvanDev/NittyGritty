using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services.ContactService
{
    public interface IContactService
    {
        Task RequestAccess();

        Task CreateContact(NGContact contact, string listName);

        Task DeleteContact(NGContact contact, string listName);

        Task Annotate(NGContact contact, NGAnnotations annotations);

        Task Pin(NGContact contact);

        Task Unpin(NGContact contact);

        Task<NGContact> GetContact(string id);

        Task<NGContact> GetContactUsingRemoteId(string remoteId, string listName);

        Task<string> GetRemoteId(NGContact contact);
    }
}
