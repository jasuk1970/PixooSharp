using PixooSharp.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Commands
{
    public class SendDisplayCommand
    {
        public string Command { get; } = "Draw/SendHttpItemList";
        public List<DisplayItem> ItemList { get; set; } = new List<DisplayItem>();
        public SendDisplayCommand(List<DisplayItem> itemList)
        {
            ItemList = itemList;
        }
    }
}
