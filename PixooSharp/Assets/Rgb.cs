namespace PixooSharp.Assets
{
    public class Rgb
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public Rgb()
        {

        }
        public Rgb(byte R, byte G, byte B)
        {
            Red = R;
            Green = G;
            Blue = B;
        }
        /// <summary>
        /// Compare colors and allow a margin to make it equel
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public bool Equals(Rgb obj, int margin = 0)
        {
            return Math.Abs(obj.Red - Red) <= margin && Math.Abs(obj.Blue - Blue) <= margin && Math.Abs(obj.Green - Green) <= margin;
        }
    }
}
