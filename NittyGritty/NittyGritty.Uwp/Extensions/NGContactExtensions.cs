using NittyGritty.Extensions;
using NittyGritty.Platform.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Storage.Streams;

namespace NittyGritty.Uwp.Extensions
{
    public static class NGContactExtensions
    {
        public static Contact ToContact(this NGContact contact)
        {
            var winContact = new Contact()
            {
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                Nickname = contact.Nickname,
                HonorificNamePrefix = contact.Prefix,
                HonorificNameSuffix = contact.Suffix,
                SourceDisplayPicture = RandomAccessStreamReference.CreateFromStream(contact.Picture.AsRandomAccessStream()),
                Notes = contact.Notes
            };
            winContact.Emails.AddRange(contact.Emails.Select(e => e.ToContactEmail()));
            winContact.Addresses.AddRange(contact.Addresses.Select(a => a.ToContactAddress()));
            winContact.ImportantDates.AddRange(contact.Dates.Select(d => d.ToContactDate()));
            winContact.JobInfo.AddRange(contact.JobInfo.Select(j => j.ToContactJobInfo()));
            winContact.Phones.AddRange(contact.Phones.Select(p => p.ToContactPhone()));
            winContact.Websites.AddRange(contact.Websites.Select(w => w.ToContactWebsite()));

            return winContact;
        }

        public static ContactEmail ToContactEmail(this NGContactEmail email)
        {
            var winEmail = new ContactEmail()
            {
                Kind = (ContactEmailKind)email.Kind,
                Address = email.Address,
                Description = email.Description
            };
            return winEmail;
        }

        public static ContactAddress ToContactAddress(this NGContactAddress address)
        {
            var winAddress = new ContactAddress()
            {
                Kind = (ContactAddressKind)address.Kind,
                StreetAddress = address.Street,
                Locality = address.Locality,
                Region = address.Region,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Description = address.Description
            };
            return winAddress;
        }

        public static ContactDate ToContactDate(this NGContactDate date)
        {
            var winDate = new ContactDate()
            {
                Kind = (ContactDateKind)date.Kind,
                Month = date.Month,
                Day = date.Day,
                Year = date.Year,
                Description = date.Description
            };
            return winDate;
        }

        public static ContactJobInfo ToContactJobInfo(this NGContactJobInfo jobInfo)
        {
            var winJobInfo = new ContactJobInfo()
            {
                Title = jobInfo.Title,
                Office = jobInfo.Office,
                Manager = jobInfo.Manager,
                Department = jobInfo.Department,
                CompanyName = jobInfo.CompanyName,
                CompanyAddress = jobInfo.CompanyAddress,
                Description = jobInfo.Description
            };
            return winJobInfo;
        }

        public static ContactPhone ToContactPhone(this NGContactPhone phone)
        {
            var winPhone = new ContactPhone()
            {
                Kind = (ContactPhoneKind)phone.Kind,
                Number = phone.Number,
                Description = phone.Description
            };
            return winPhone;
        }

        public static ContactWebsite ToContactWebsite(this NGContactWebsite website)
        {
            var winWebsite = new ContactWebsite()
            {
                RawValue = website.Raw,
                Uri = website.GetUri(),
                Description = website.Description
            };
            return winWebsite;
        }

        public static async Task<NGContact> ToNGContact(this Contact contact)
        {
            var ngContact = new NGContact()
            {
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                Nickname = contact.Nickname,
                Prefix = contact.HonorificNamePrefix,
                Suffix = contact.HonorificNameSuffix,
                Picture = (await contact.SourceDisplayPicture.OpenReadAsync()).AsStream(),
                Notes = contact.Notes
            };
            ngContact.Emails.AddRange(contact.Emails.Select(e => e.ToNGContactEmail()));
            ngContact.Addresses.AddRange(contact.Addresses.Select(a => a.ToNGContactAddress()));
            ngContact.Dates.AddRange(contact.ImportantDates.Select(d => d.ToNGContactDate()));
            ngContact.JobInfo.AddRange(contact.JobInfo.Select(j => j.ToNGContactJobInfo()));
            ngContact.Phones.AddRange(contact.Phones.Select(p => p.ToNGContactPhone()));
            ngContact.Websites.AddRange(contact.Websites.Select(w => w.ToNGContactWebsite()));

            return ngContact;
        }

        public static NGContactEmail ToNGContactEmail(this ContactEmail email)
        {
            var ngEmail = new NGContactEmail()
            {
                Kind = (NGContactEmailKind)email.Kind,
                Address = email.Address,
                Description = email.Description
            };
            return ngEmail;
        }

        public static NGContactAddress ToNGContactAddress(this ContactAddress address)
        {
            var ngAddress = new NGContactAddress()
            {
                Kind = (NGContactAddressKind)address.Kind,
                Street = address.StreetAddress,
                Locality = address.Locality,
                Region = address.Region,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Description = address.Description
            };
            return ngAddress;
        }

        public static NGContactDate ToNGContactDate(this ContactDate date)
        {
            var ngDate = new NGContactDate()
            {
                Kind = (NGContactDateKind)date.Kind,
                Month = date.Month,
                Day = date.Day,
                Year = date.Year,
                Description = date.Description
            };
            return ngDate;
        }

        public static NGContactJobInfo ToNGContactJobInfo(this ContactJobInfo jobInfo)
        {
            var ngJobInfo = new NGContactJobInfo()
            {
                Title = jobInfo.Title,
                Office = jobInfo.Office,
                Manager = jobInfo.Manager,
                Department = jobInfo.Department,
                CompanyName = jobInfo.CompanyName,
                CompanyAddress = jobInfo.CompanyAddress,
                Description = jobInfo.Description
            };
            return ngJobInfo;
        }

        public static NGContactPhone ToNGContactPhone(this ContactPhone phone)
        {
            var ngPhone = new NGContactPhone()
            {
                Kind = (NGContactPhoneKind)phone.Kind,

                Number = phone.Number,
                Description = phone.Description
            };
            return ngPhone;
        }

        public static NGContactWebsite ToNGContactWebsite(this ContactWebsite website)
        {
            var ngWebsite = new NGContactWebsite()
            {
                Raw = website.RawValue,
                Description = website.Description
            };
            return ngWebsite;
        }

    }
}
