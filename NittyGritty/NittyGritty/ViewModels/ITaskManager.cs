using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NittyGritty.ViewModels
{
    public interface ITaskManager
    {
        Task<TResult> Run<TResult>(Func<CancellationToken, Task<TResult>> task);

        Task<TResult> Run<TResult>(Task<TResult> task);

        Task Run(Func<CancellationToken, Task> task);

        Task Run(Task task);
    }
}
