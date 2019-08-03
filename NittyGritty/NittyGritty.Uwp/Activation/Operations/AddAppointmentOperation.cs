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
    public class AddAppointmentOperation : AppointmentOperation, IViewOperation<AppointmentsProviderAddAppointmentActivatedEventArgs>
    {
        public AddAppointmentOperation(Type view) : base(AppointmentAction.Add, view)
        {
        }

        public async Task Run(AppointmentsProviderAddAppointmentActivatedEventArgs args, Frame frame)
        {
            var payload = new AddAppointmentPayload(args.AddAppointmentOperation);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            var viewConfig = new MultiViewConfiguration<AddAppointmentPayload>(View, (p) => true);
            await viewConfig.Show(payload, currentApplicationViewId, frame);
        }
    }
}
