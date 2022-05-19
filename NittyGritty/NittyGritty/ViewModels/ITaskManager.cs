using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NittyGritty.ViewModels
{
    public interface ITaskManager
    {
        Task<TResult> Run<TResult>(Func<CancellationToken, Task<TResult>> task, CancellationTokenSource tokenSource = null);

        Task<TResult> Run<TResult>(Task<TResult> task);

        Task Run(Func<CancellationToken, Task> task, CancellationTokenSource tokenSource = null);

        Task Run(Task task);
    }
}
