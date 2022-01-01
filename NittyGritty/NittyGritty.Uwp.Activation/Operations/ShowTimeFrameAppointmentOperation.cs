using System;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ShowTimeFrameAppointmentOperation : AppointmentOperation, IViewOperation<AppointmentsProviderShowTimeFrameActivatedEventArgs>
    {
        public ShowTimeFrameAppointmentOperation(Type view) : base(AppointmentAction.ShowTimeFrame, view)
        {

        }

        public async Task Run(AppointmentsProviderShowTimeFrameActivatedEventArgs args, Frame frame)
        {
            var payload = new ShowTimeFrameAppointmentPayload(args.TimeToShow, args.Duration);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            var viewConfig = new MultiViewConfiguration<ShowTimeFrameAppointmentPayload>(View, (p) => true);
            await viewConfig.Show(payload, currentApplicationViewId, frame);
        }

        public override async Task Run(IAppointmentsProviderActivatedEventArgs args, Frame frame)
        {
            if(args is AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrameArgs)
            {
                await Run(showTimeFrameArgs, frame);
            }
            else
            {
                throw new ArgumentException("Args is not of AppointmentsProviderShowTimeFrameActivatedEventArgs type");
            }
        }
    }
}
