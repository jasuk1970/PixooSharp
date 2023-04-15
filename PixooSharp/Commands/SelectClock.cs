using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Commands
{
    internal class SendSelectClock
    {
        public string Command { get; } = "Channel/SetClockSelectId";
        public int ClockId{ get; set; } = 3;
        public SendSelectClock(int clockId) 
        {
            ClockId = clockId;
        }
    }
}
