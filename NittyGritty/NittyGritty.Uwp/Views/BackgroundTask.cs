using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace NittyGritty.Uwp.Views
{
    public abstract class BackgroundTask
    {
        public abstract void Register();

        public bool Match(string name)
        {
            return name == GetType().Name;
        }

        public Task RunAsync(IBackgroundTaskInstance taskInstance)
        {
            Attach(taskInstance);

            return RunAsyncInternal(taskInstance);
        }

        private void Attach(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
        }

        protected abstract Task RunAsyncInternal(IBackgroundTaskInstance taskInstance);

        protected abstract void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason);
    }
}
