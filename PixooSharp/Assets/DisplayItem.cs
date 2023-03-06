using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Assets
{
    public class DisplayItem
    {
        public int TextId { get; set; }
        public int type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int dir { get; set; }
        public int font { get; set; } = 2;
        public int TextWidth { get; set; }
        public int TextHeight { get; set; }
        public int speed { get; set; }
        public int align { get; set; }
        public string color { get; set; } = "#FF0000";
        public string TextString { get; set; } = "";
    }
}
