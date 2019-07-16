using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;

namespace NittyGritty.Uwp.Services.Activation
{
    public class ToastNotificationActivationHandler : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public Func<QueryString, ValueSet, Task> ProcessToast { get; set; }

        public override async Task HandleAsync(ToastNotificationActivatedEventArgs args)
        {
            await ProcessToast?.Invoke(QueryString.Parse(args.Argument), args.UserInput);
        }
    }
}
