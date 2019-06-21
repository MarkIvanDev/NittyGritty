using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace NittyGritty.ViewModels
{
    public interface ICancellationManager
    {
        Collection<CancellationTokenSource> CancellationTokenSources { get; }

        void RegisterCancellationTokenSource(CancellationTokenSource cancellationTokenSource);

        CancellationTokenSource CreateCancellationTokenSource();

        void DeregisterCancellationTokenSource(CancellationTokenSource cancellationTokenSource);

        void CancelAll();
    }
}
