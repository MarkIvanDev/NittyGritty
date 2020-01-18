using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Services
{
    public interface IFileService
    {
        Task RequestAccess();
    }
}
