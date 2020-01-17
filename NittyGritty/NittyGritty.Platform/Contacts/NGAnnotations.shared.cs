using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Contacts
{
    [Flags]
    public enum NGAnnotations : uint
    {
        //
        // Summary:
        //     None
        None = 0,
        //
        // Summary:
        //     Get the contact profile.
        ContactProfile = 1,
        //
        // Summary:
        //     Send an SMS/MMS message.
        Message = 2,
        //
        // Summary:
        //     Make and audio call.
        AudioCall = 4,
        //
        // Summary:
        //     Make a video call.
        VideoCall = 8,
        //
        // Summary:
        //     Access social media feeds.
        SocialFeeds = 16,
        //
        // Summary:
        //     Share the contact.
        Share = 32
    }
}
