using IronSoftware.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixooSharp.Assets
{
    public class ImageFrame
    {
        private readonly int _pixelCount;
        private readonly Font _pico8 = new Font();
        private Rgb[] _imageBase = new Rgb[0];
        private Rgb[] _imageData = new Rgb[0];
        public int ScreenSize { get; set; }
        public bool debug { get; set; }
        public ImageFrame(PixooSize pixooSize)
        {
            ScreenSize = (int)pixooSize;
            _pixelCount = ScreenSize * ScreenSize;
            _imageData = new Rgb[ScreenSize * ScreenSize];
            _pico8 = new Font();
            // Set default boot colour to black
            Fill(Palette.Black);
        }
        public void Clear()
        {
            Fill(Palette.Black);
        }
        public void SetBaseFrame()
        {
            _imageBase = _imageData.ToArray(); 
        }
        public void RestoreBaseFrame()
        {
            _imageData = _imageBase.ToArray();
        }
        public void Fill(Rgb fillColour)
        {
            for (int buffPos = 0; buffPos < _pixelCount; buffPos += 1)
            {
                _imageData[buffPos] = fillColour;
            }
        }
        public void DrawFilledRectangle(int topX, int topY, int bottomX, int bottomY, Rgb colour)
        {
            for (var y = topY; y <= bottomY; y++)
            {
                for (var x = topX; x <= bottomX; x++)
                {
                    DrawPixel(x, y, colour);
                }
            }
        }
        public void DrawLine(int startX, int startY, int endX, int endY, Rgb colour)
        {
            // Calculate the amount of steps needed between the points to draw a nice line
            int amountOfSteps = MinimumAmountOfSteps(startX, startY, endX, endY);

            // Iterate over them and create a nice set of pixels
            for (int step = 0; step < amountOfSteps; step++)
            {
                decimal interpolant = 0;
                if (amountOfSteps != 0)
                {
                    interpolant = ((decimal)step / (decimal)amountOfSteps);
                }
                // Add a pixel as a rounded location
                Coord lerpResult = LerpLocation(startX, startY, endX, endY, interpolant);
                Coord roundResult = RoundLocation(lerpResult.X, lerpResult.Y);
                DrawPixel((int)roundResult.X, (int)roundResult.Y, colour);
            }
        }
        public decimal Lerp(decimal start, decimal end, decimal interpolant)
        {
            return (start + interpolant * (end - start));
        }

        public Coord LerpLocation(decimal startX, decimal startY, decimal endX, decimal endY, decimal interpolant)
        {
            Coord coord = new Coord()
            {
                X = Lerp(startX, endX, interpolant),
                Y = Lerp(startY, endY, interpolant)
            };
            return coord;
        }

        public Coord RoundLocation(decimal x, decimal y)
        {
            Coord result = new Coord()
            {
                X = (int)Math.Round(x),
                Y = (int)Math.Round(y)
            };
            return result;
        }

        public int MinimumAmountOfSteps(int startX, int startY, int endX, int endY)
        {
            return Math.Max(Math.Abs(startX - endX), Math.Abs(startY - endY));
        }

        public void DrawText(int x, int y, Rgb colour, string text)
        {
            var characters = text.ToCharArray();
            int index = 0;
            foreach (char c in characters)
            {
                if (c != ' ')
                {
                    DrawCharacter((index * 4) + x, y, colour, c);
                }
                index++;
            }
        }
        public void DrawCharacter(int x, int y, Rgb colour, char character)
        {
            var matrix = _pico8.GetCharacterMatrix(character);
            if (matrix != null)
            {
                for (int index = 0; index < matrix.Length; index++)
                {
                    var bit = matrix[index];
                    if (bit == 1)
                    {
                        int local_x = index % 3;
                        int local_y = (index / 3);
                        DrawPixel(x + local_x, y + local_y, colour);
                    }
                }
            }
        }
        public void DrawPixel(int x, int y, Rgb colour)
        {
            if (x >= 0 && x < ScreenSize && y >= 0 && y < ScreenSize)
            {
                var index = x + (y * ScreenSize);
                DrawPixelAtIndex(index, colour);
            }
            else
            {
                if (debug)
                {
                    int limit = ScreenSize - 1;
                    Console.WriteLine("Error: Invalid coordinates given: ({0}, {1}) (maximum coordinates are ({3}, {3})", x, y, limit);
                }
            }
        }
        public Rgb GetPixel(int x, int y)
        {
            var colour = new Rgb();
            if (x >= 0 && x < ScreenSize && y >= 0 && y < ScreenSize)
            {
                var index = x + (y * ScreenSize);
                colour = _imageData[index];
            }
            else
            {
                if (debug)
                {
                    int limit = ScreenSize - 1;
                    Console.WriteLine("Error: Invalid coordinates given: ({0}, {1}) (maximum coordinates are ({3}, {3})", x, y, limit);
                }
            }
            return colour;
        }
        public void DrawPixelAtIndex(int index, Rgb colour)
        {
            if (index < 0 || index >= _pixelCount)
            {
                if (debug)
                {
                    var limit = _pixelCount - 1;
                    Console.WriteLine("Error: Invalid index given: ({0}) (maximum coordinates are ({1})", index, limit);
                }
            }
            else
            {
                _imageData[index] = colour;
            }
        }
        public string Base64Buffer
        {
            get
            {
                Byte[] sendBuffer = new byte[_pixelCount * 3];
                int ySkip = ScreenSize * 3;
                for (var y = 0; y < ScreenSize; y++)
                {
                    if (debug)
                    {
                        Console.Write(y.ToString());
                    }
                    for (var x = 0; x < ScreenSize; x++)
                    {
                        if (debug)
                        {
                            Console.Write(".");
                        }
                        int sendIndex = (y * ySkip) + (x * 3);
                        int bufferIndex = (y * ScreenSize) + x;
                        sendBuffer[sendIndex] = _imageData[bufferIndex].Red;
                        sendBuffer[sendIndex + 1] = _imageData[bufferIndex].Green;
                        sendBuffer[sendIndex + 2] = _imageData[bufferIndex].Blue;
                    }
                    if (debug)
                    {
                        Console.WriteLine();
                    }
                }
                return Convert.ToBase64String(sendBuffer, 0, sendBuffer.Length);
            }
        }
        public void LoadPNG(string fileName)
        {
            if (File.Exists(fileName))
            {
                var bitmap64 = AnyBitmap.FromFile(fileName);
                if(bitmap64.Width > ScreenSize || bitmap64.Height > ScreenSize)
                {
                    bitmap64 = new AnyBitmap(bitmap64, ScreenSize, ScreenSize);
                }
                //        Todo Load full animation    bitmap.GetAllFrames
                for (int x = 0; x < ScreenSize; x++)
                {
                    for (int y = 0; y < ScreenSize; y++)
                    {
                        var clr = bitmap64.GetPixel(x, y);

                        var pixelColor = new Rgb();
                        pixelColor.Red = clr.R;
                        pixelColor.Green = clr.G;
                        pixelColor.Blue = clr.B;
                        DrawPixel(x, y, pixelColor);
                    }
                }
                bitmap64.Dispose();
            } else if (debug)
            {
                Console.WriteLine($"Image {fileName} not found.");
            }
        }
        public void PasteImage(ImageFrame sprite, int xOffset, int yOffset, Rgb? transparantColor = null, int marginTransarancy = 0)
        {
            for (int x = 0; x < sprite.ScreenSize; x++)
            {
                for (int y = 0; y < sprite.ScreenSize; y++)
                {
                    var pixelColor = sprite.GetPixel(x, y);
                    if(transparantColor == null || !pixelColor.Equals(transparantColor, marginTransarancy))
                    {
                        DrawPixel(x + xOffset, y + yOffset, pixelColor);
                    }
                }
            }
        }
    }
}
