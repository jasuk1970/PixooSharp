using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Commands
{
    internal class SendPlayBuzzer
    {
        public string Command { get; } = "Device/PlayBuzzer";
        public int ActiveTimeInCycle { get; set; }
        public int OffTimeInCycle { get; set; }
        public int PlayTotalTime { get; set; }

        public SendPlayBuzzer(int duration, int onTime, int offTime)
        {
            ActiveTimeInCycle = onTime;
            OffTimeInCycle = offTime;
            PlayTotalTime = duration;
        }
    }
}
