using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    public abstract class ActivationHandler<T> : ActivationHandler where T : class, IActivatedEventArgs
    {
        public Func<T, Task> Handle { get; set; }

        public override async Task HandleAsync(object args)
        {
            await Handle.Invoke(args as T);
        }

        public override bool CanHandle(object args)
        {
            return args is T;
        }
    }
}
