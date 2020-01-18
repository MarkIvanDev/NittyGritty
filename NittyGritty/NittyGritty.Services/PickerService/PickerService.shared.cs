using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

namespace NittyGritty.Services
{
    public partial class PickerService : IPickerService
    {
        private static readonly object _instanceLock = new object();        private static PickerService _default;
        public static PickerService Default        {            get            {                if (_default == null)                {                    lock (_instanceLock)                    {                        if (_default == null)                        {                            _default = new PickerService();                        }                    }                }                return _default;            }        }

        public async Task<IFile> Open(IList<string> fileTypes)
        {
            return await PlatformOpen(fileTypes);
        }

        public async Task<IFile> Save(IDictionary<string, IList<string>> fileTypes)
        {
            return await PlatformSave(fileTypes);
        }
    }
}
