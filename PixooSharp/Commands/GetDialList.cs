using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Commands
{
    internal class GetDialList
    {
        //public string Command { get; } = "Channel/GetDialList";
        public string DialType { get; set; } = "CUSTOM";
        public int Page { get; set; } = 1;
    }
}
