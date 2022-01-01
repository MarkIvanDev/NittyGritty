using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace NittyGritty.Uwp.Activation.Operations
{
    public abstract class BackgroundTask
    {
        public BackgroundTask(string name = null)
        {
            Name = name ?? GetType().FullName;
        }

        public string Name { get; }

        public virtual void Register(BackgroundTaskBuilder builder)
        {
            builder.Register();
        }

        public abstract Task Run(IBackgroundTaskInstance taskInstance);
    }
}
