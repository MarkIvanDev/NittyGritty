using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public class ShowTimeFrameAppointmentPayload
    {
        public ShowTimeFrameAppointmentPayload(DateTimeOffset startTime, TimeSpan duration)
        {
            StartTime = startTime;
            Duration = duration;
        }

        public DateTimeOffset StartTime { get; }

        public TimeSpan Duration { get; }
    }
}
