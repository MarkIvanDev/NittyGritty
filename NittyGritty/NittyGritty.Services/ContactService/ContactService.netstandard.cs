using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Contacts;

namespace NittyGritty.Services
{
    public partial class ContactService
    {
        Task PlatformAnnotate(NGContact contact, NGAnnotations annotations)
        {
            throw new NotImplementedException();
        }

        Task PlatformCreateContact(NGContact contact, string listName)
        {
            throw new NotImplementedException();
        }

        Task PlatformDeleteContact(NGContact contact, string listName)
        {
            throw new NotImplementedException();
        }

        Task<NGContact> PlatformGetContact(string id)
        {
            throw new NotImplementedException();
        }

        Task<NGContact> PlatformGetContactUsingRemoteId(string remoteId, string listName)
        {
            throw new NotImplementedException();
        }

        Task<string> PlatformGetRemoteId(NGContact contact)
        {
            throw new NotImplementedException();
        }

        Task PlatformPin(NGContact contact)
        {
            throw new NotImplementedException();
        }

        Task PlatformRequestAccess()
        {
            throw new NotImplementedException();
        }

        Task PlatformUnpin(NGContact contact)
        {
            throw new NotImplementedException();
        }
    }
}
