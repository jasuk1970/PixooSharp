// See https://aka.ms/new-console-template for more information

using PixooSharp;
using PixooSharp.Assets;

var pixoo = new Pixoo64("172.24.1.241", 64, true);
pixoo.DrawLine(0, 0, 31, 0, Palette.White);
pixoo.DrawLine(31, 0, 31, 31, Palette.White);
pixoo.DrawLine(31, 31, 0, 31, Palette.White);
pixoo.DrawLine(0, 31, 0, 0, Palette.White);
pixoo.DrawText(0, 34, Palette.Green, "Hello World2!");
pixoo.DrawFilledRectangle(10, 10, 20, 20, Palette.Green);
// We are not creating a gif to animate so reset to make sure the image Id will work
await pixoo.SendResetGif();
// We now specify the frame Id as part of the SendBuffer
await pixoo.SendBufferAsync(0);
await pixoo.SendTextAsync(0, 0, Direction.RIGHT, "This is a message that scrolls across the screen. I hope it works!", Palette.White);
Thread.Sleep(20000);
await pixoo.SendClearTextAsync();