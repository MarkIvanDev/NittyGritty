using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public class ShowAppointmentDetailsPayload
    {
        public ShowAppointmentDetailsPayload(string roamingId, string localId, DateTimeOffset? startDate)
        {
            RoamingId = roamingId;
            LocalId = localId;
            StartDate = startDate;
        }

        public string RoamingId { get; }

        public string LocalId { get; }

        public DateTimeOffset? StartDate { get; }
    }
}
