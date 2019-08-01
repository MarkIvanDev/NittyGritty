using NittyGritty.Platform.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IShareTargetPayload
    {
        string Title { get; }

        string Description { get; }

        string Id { get; }

        IReadOnlyCollection<NGContact> Contacts { get; }



        void ShareStarted();

        void ShareFailed(string error);

        void ShareCompleted();
    }
}
