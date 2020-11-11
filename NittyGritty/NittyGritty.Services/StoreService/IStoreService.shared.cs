using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public interface IStoreService
    {
        string AppId { get; }

        Task RequestRating();

        Task<bool> CheckForUpdates();

        Task<bool> CheckForMandatoryUpdates();

        Task DownloadUpdates();

        Task DownloadAndInstallUpdates();

    }
}
