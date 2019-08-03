using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Platform;
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
    public class ShowAppointmentDetailsOperation : AppointmentOperation, IViewOperation<AppointmentsProviderShowAppointmentDetailsActivatedEventArgs>
    {
        public ShowAppointmentDetailsOperation(Type view) : base(AppointmentAction.ShowDetails, view)
        {
        }

        public async Task Run(AppointmentsProviderShowAppointmentDetailsActivatedEventArgs args, Frame frame)
        {
            var payload = new ShowAppointmentDetailsPayload(args.LocalId, args.InstanceStartDate);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            var viewConfig = new MultiViewConfiguration<ShowAppointmentDetailsPayload>(View, (p) => true);
            await viewConfig.Show(payload, currentApplicationViewId, frame);
        }

        public override async Task Run(IAppointmentsProviderActivatedEventArgs args, Frame frame)
        {
            if (args is AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetailsArgs)
            {
                await Run(showDetailsArgs, frame);
            }
            else
            {
                throw new ArgumentException("Args is not of AppointmentsProviderShowAppointmentDetailsActivatedEventArgs type");
            }
        }
    }
}
