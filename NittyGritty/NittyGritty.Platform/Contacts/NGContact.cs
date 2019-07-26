using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    public class NGContact : ObservableObject
    {

        private string _id;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _remoteId;

        public string RemoteId
        {
            get { return _remoteId; }
            set { Set(ref _remoteId, value); }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { Set(ref _firstName, value); }
        }

        private string _middleName;

        public string MiddleName
        {
            get { return _middleName; }
            set { Set(ref _middleName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { Set(ref _lastName, value); }
        }

        private string _nickName;

        public string Nickname
        {
            get { return _nickName; }
            set { Set(ref _nickName, value); }
        }

        private string _prefix;

        public string Prefix
        {
            get { return _prefix; }
            set { Set(ref _prefix, value); }
        }

        private string _suffix;

        public string Suffix
        {
            get { return _suffix; }
            set { Set(ref _suffix, value); }
        }

        private Collection<NGContactEmail> _emails;

        public Collection<NGContactEmail> Emails
        {
            get { return _emails ?? (_emails = new Collection<NGContactEmail>()); }
        }

        private Collection<NGContactAddress> _addresses;

        public Collection<NGContactAddress> Addresses
        {
            get { return _addresses ?? (_addresses = new Collection<NGContactAddress>()); }
        }

        private Collection<NGContactDate> _dates;

        public Collection<NGContactDate> Dates
        {
            get { return _dates ?? (_dates = new Collection<NGContactDate>()); }
        }

        private Collection<NGContactJobInfo> _jobInfo;

        public Collection<NGContactJobInfo> JobInfo
        {
            get { return _jobInfo ?? (_jobInfo = new Collection<NGContactJobInfo>()); }
        }

        private Collection<NGContactPhone> _phones;

        public Collection<NGContactPhone> Phones
        {
            get { return _phones ?? (_phones = new Collection<NGContactPhone>()); }
        }

        private Collection<NGContactWebsite> _websites;

        public Collection<NGContactWebsite> Websites
        {
            get { return _websites ?? (_websites = new Collection<NGContactWebsite>()); }
        }

        private Stream _picture;

        public Stream Picture
        {
            get { return _picture; }
            set { Set(ref _picture, value); }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { Set(ref _notes, value); }
        }

    }
}
