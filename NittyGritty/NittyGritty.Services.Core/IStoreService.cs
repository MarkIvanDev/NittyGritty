using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services.Core
{
    public interface IStoreService
    {
        Task RequestRating();

        Task<bool> CheckForUpdates();

        Task<bool> CheckForMandatoryUpdates();

        Task DownloadUpdates();

        Task DownloadAndInstallUpdates();

    }
}
