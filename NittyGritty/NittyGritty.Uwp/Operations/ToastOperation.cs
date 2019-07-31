using NittyGritty.Models;
using NittyGritty.Views.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class ToastOperation : ViewConfigurableOperation<ToastPayload>
    {
        public ToastOperation(string key)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty, or whitespace", nameof(key));
            }

            Key = key;
        }

        public string Key { get; }

        public virtual async Task Run(ToastNotificationActivatedEventArgs args, Frame frame)
        {
            var query = QueryString.Parse(args.Argument);
            if(!query.Contains(Key))
            {
                return;
            }

            var value = query[Key];
            var payload = new ToastPayload(args.UserInput, query);

            var configuration = GetConfiguration(value);
            await configuration.Run(payload, args.CurrentlyShownApplicationViewId, frame);
        }
    }
}
