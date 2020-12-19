using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace NittyGritty.Platform.Launcher
{
    public class NGAppInfo : ObservableObject, IAppInfo
    {
        public NGAppInfo(AppInfo appInfo)
        {
            AppInfo = appInfo;
            Id = appInfo.Id;
            DisplayName = appInfo.DisplayInfo.DisplayName;
            PackageFamilyName = appInfo.PackageFamilyName;
            Description = appInfo.DisplayInfo.Description;
            AppUserModelId = appInfo.AppUserModelId;
        }

        public AppInfo AppInfo { get; }

        public string Id { get; }

        public string DisplayName { get; }

        public string PackageFamilyName { get; }

        public string Description { get; }

        public string AppUserModelId { get; }

        private Stream _logo;

        public Stream Logo
        {
            get { return _logo; }
            set { Set(ref _logo, value); }
        }

        public async Task<Stream> GetLogo(Size size)
        {
            var logo = await AppInfo.DisplayInfo.GetLogo(
                new Windows.Foundation.Size(size.Width, size.Height)).OpenReadAsync();
            return logo.AsStream();
        }
    }
}
