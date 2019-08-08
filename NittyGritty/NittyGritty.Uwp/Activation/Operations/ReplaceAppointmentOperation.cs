using NittyGritty.Uwp.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ReplaceAppointmentOperation : AppointmentOperation, IViewOperation<AppointmentsProviderReplaceAppointmentActivatedEventArgs>
    {
        public ReplaceAppointmentOperation(Type view) : base(AppointmentAction.Replace, view)
        {
        }

        public async Task Run(AppointmentsProviderReplaceAppointmentActivatedEventArgs args, Frame frame)
        {
            var payload = new ReplaceAppointmentPayload(args.ReplaceAppointmentOperation);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            var viewConfig = new MultiViewConfiguration<ReplaceAppointmentPayload>(View, (p) => true);
            await viewConfig.Show(payload, currentApplicationViewId, frame);
        }

        public override async Task Run(IAppointmentsProviderActivatedEventArgs args, Frame frame)
        {
            if (args is AppointmentsProviderReplaceAppointmentActivatedEventArgs replaceArgs)
            {
                await Run(replaceArgs, frame);
            }
            else
            {
                throw new ArgumentException("Args is not of AppointmentsProviderReplaceAppointmentActivatedEventArgs type");
            }
        }
    }
}
