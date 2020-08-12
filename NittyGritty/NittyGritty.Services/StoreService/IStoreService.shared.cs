using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public interface IStoreService
    {

        Task<bool> CheckForUpdates();

        Task<bool> CheckForMandatoryUpdates();

        Task DownloadUpdates();

        Task DownloadAndInstallUpdates();

    }
}
