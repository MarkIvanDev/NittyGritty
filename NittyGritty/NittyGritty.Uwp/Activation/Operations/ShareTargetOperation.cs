using NittyGritty.Uwp.Activation.Operations.Configurations;
using NittyGritty.Uwp.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ShareTargetOperation : IViewOperation<ShareTargetActivatedEventArgs>
    {
        private readonly Dictionary<string, ShareTargetConfiguration> configurations;

        public ShareTargetOperation(ShareTargetType type, params ShareTargetConfiguration[] configurations)
        {
            Type = type;
            this.configurations = new Dictionary<string, ShareTargetConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.DataFormat, config);
            }
        }

        public ShareTargetType Type { get; }

        public async Task Run(ShareTargetActivatedEventArgs args, Frame frame)
        {
            var supported = new List<ShareTargetConfiguration>();
            foreach (var df in args.ShareOperation.Data.AvailableFormats)
            {
                if (configurations.TryGetValue(df, out var config))
                {
                    supported.Add(config);
                }
            }
            var picked = supported.OrderByDescending(s => s.Priority).FirstOrDefault();
            if(picked != null)
            {
                var payload = new ShareTargetPayload(args.ShareOperation);
            }
        }

        private ShareTargetConfiguration GetConfiguration(string dataFormat)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(dataFormat, out var config))
                {
                    return config;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackConfig))
                    {
                        return fallbackConfig;
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"No configuration for data format: {dataFormat}. Did you forget to register a ShareTargetConfiguration?",
                            nameof(dataFormat));
                    }
                }
            }
        }
    }

    public enum ShareTargetType
    {
        Regular = 0,
        HasContacts = 1
    }
}
