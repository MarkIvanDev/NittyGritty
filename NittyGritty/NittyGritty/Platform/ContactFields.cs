using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform
{
    public static class ContactFields
    {
        public static string FirstName { get; } = nameof(FirstName);

        public static string MiddleName { get; } = nameof(MiddleName);

        public static string LastName { get; } = nameof(LastName);

        public static string Nickname { get; } = nameof(Nickname);

        public static string Prefix { get; } = nameof(Prefix);

        public static string Suffix { get; } = nameof(Suffix);

        public static string Emails { get; } = nameof(Emails);

        public static string Addresses { get; } = nameof(Addresses);

        public static string Dates { get; } = nameof(Dates);

        public static string Jobs { get; } = nameof(Jobs);

        public static string Phones { get; } = nameof(Phones);

        public static string Websites { get; } = nameof(Websites);

        public static string Picture { get; } = nameof(Picture);

        public static string Notes { get; } = nameof(Notes);
    }
}
