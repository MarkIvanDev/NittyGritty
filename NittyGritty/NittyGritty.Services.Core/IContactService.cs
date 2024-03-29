﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Contacts;

namespace NittyGritty.Services.Core
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
