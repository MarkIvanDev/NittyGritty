using NittyGritty.Uwp.Platform;
using NittyGritty.Uwp.Platform.Payloads;
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
    public class RemoveAppointmentOperation : AppointmentOperation, IViewOperation<AppointmentsProviderRemoveAppointmentActivatedEventArgs>
    {
        public RemoveAppointmentOperation(Type view) : base(AppointmentAction.Remove, view)
        {
        }

        public async Task Run(AppointmentsProviderRemoveAppointmentActivatedEventArgs args, Frame frame)
        {
            var payload = new RemoveAppointmentPayload(args.RemoveAppointmentOperation);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            var viewConfig = new MultiViewConfiguration<RemoveAppointmentPayload>(View, (p) => true);
            await viewConfig.Show(payload, currentApplicationViewId, frame);
        }
    }
}
