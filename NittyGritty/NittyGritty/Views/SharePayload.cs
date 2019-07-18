using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NittyGritty.Views
{
    public class SharePayload
    {
        private SharePayload(string title)
        {
            Title = title;
        }

        public static SharePayload Create(string title)
        {
            return new SharePayload(title);
        }

        public string Title { get; }

        public string Text { get; private set; }

        public string Html { get; private set; }

        public string Rtf { get; private set; }

        public Uri AppLink { get; private set; }

        public Uri WebLink { get; private set; }

        public Stream Bitmap { get; private set; }

        public IEnumerable<Stream> Files { get; private set; }

        public SharePayload SetText(string text)
        {
            Text = text;
            return this;
        }

        public SharePayload SetHtml(string html)
        {
            Html = html;
            return this;
        }

        public SharePayload SetRtf(string rtf)
        {
            Rtf = rtf;
            return this;
        }

        public SharePayload SetAppLink(Uri appLink)
        {
            AppLink = appLink;
            return this;
        }

        public SharePayload SetWebLink(Uri webLink)
        {
            WebLink = webLink;
            return this;
        }

        public SharePayload SetBitmap(Stream bitmap)
        {
            Bitmap = bitmap;
            return this;
        }

        public SharePayload SetFiles(IEnumerable<Stream> files)
        {
            Files = files;
            return this;
        }


    }
}
