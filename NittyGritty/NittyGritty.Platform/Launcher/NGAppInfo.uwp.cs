using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace NittyGritty.Platform.Launcher
{
    public class NGAppInfo : IAppInfo
    {
        private readonly AppInfo appInfo;

        public NGAppInfo(AppInfo appInfo)
        {
            this.appInfo = appInfo;
            Id = appInfo.Id;
            DisplayName = appInfo.DisplayInfo.DisplayName;
            PackageFamilyName = appInfo.PackageFamilyName;
            Description = appInfo.DisplayInfo.Description;
            AppUserModelId = appInfo.AppUserModelId;
        }

        public string Id { get; }

        public string DisplayName { get; }

        public string PackageFamilyName { get; }

        public string Description { get; }

        public string AppUserModelId { get; }

        public async Task<Stream> GetLogo(Size size)
        {
            var logo = await appInfo.DisplayInfo.GetLogo(
                new Windows.Foundation.Size(size.Width, size.Height)).OpenReadAsync();
            return logo.AsStream();
        }
    }
}
