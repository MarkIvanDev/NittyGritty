using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class ActivationHandler<T> where T : class, IActivatedEventArgs
    {
        protected Func<T, Task> Handler { get; set; }

        public async Task HandleAsync(T args)
        {
            await Handler?.Invoke(args);
        }

        public virtual bool CanHandle(T args)
        {
            return true;
        }
    }
}
