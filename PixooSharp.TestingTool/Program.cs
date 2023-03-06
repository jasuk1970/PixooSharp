using PixooSharp;
using PixooSharp.Assets;

var pixoo = new Pixoo64("pixoo", true);

/*
 * 
 * Original Draw lines and boxes demo
 * 
 */

var imageFrame = new ImageFrame(PixooSize.Pix64);

imageFrame.DrawLine(0, 0, 31, 0, Palette.White);
imageFrame.DrawLine(31, 0, 31, 31, Palette.White);
imageFrame.DrawLine(31, 31, 0, 31, Palette.White);
imageFrame.DrawLine(0, 31, 0, 0, Palette.White);
imageFrame.DrawFilledRectangle(10, 10, 20, 20, Palette.Green);

imageFrame.DrawText(0, 34, Palette.Green, "Hello World!");

// We now specify the picture Id as part of the SendBuffer
await pixoo.SendBufferAsync(1, imageFrame);

Thread.Sleep(3000);

// Scrolling text
await pixoo.SendTextAsync(0, 0, Direction.RIGHT, "This is a message that scrolls across the screen. I hope it works!", Palette.White);
Thread.Sleep(6000);

// clear all
await pixoo.SendClearTextAsync();
await pixoo.SendResetGif();


/*
 * 
 *  SETUP a background + menu panel and add an animation
 *  
 */

// Setup a baseframe and paste images onto it using a fixed color as transparancy
imageFrame.LoadPNG(@"Images/background.png");
var sprite = new ImageFrame(PixooSize.Pix64);

// Paste a second image, and use the color white as transparancy. 
sprite.LoadPNG(@"Images/menupanel.png");
imageFrame.PasteImage(sprite, 0, 0, Palette.Red);

// set this as the baseframe
imageFrame.SetBaseFrame();

// Add menu text
imageFrame.DrawText(6, 7, Palette.Green, "Hello World");
await pixoo.SendBufferAsync(1, imageFrame, 0, 1, 0);
Thread.Sleep(4000);

await pixoo.SendResetGif();

// wake people up
await pixoo.PlayBuzzer(3000, 1000, 500);

// Now reset to the base image
imageFrame.RestoreBaseFrame();

// Load and add a 'sprite' using Red as transparacy color
sprite = new ImageFrame(PixooSize.Pix32);
sprite.LoadPNG(@"Images/dragon.png");
imageFrame.PasteImage(sprite, 13, 25, Palette.Red, 30);

// Set title
imageFrame.DrawText(12, 7, Palette.Green, "Dragons!");

// Add this image as first frame of animation
await pixoo.SendBufferAsync(1, imageFrame, 0, 2, 1000);

// redo with a chicken
imageFrame.RestoreBaseFrame();
sprite = new ImageFrame(PixooSize.Pix32);
sprite.LoadPNG(@"Images/chicken.png");
imageFrame.PasteImage(sprite, 13, 25, Palette.Red, 30);
imageFrame.DrawText(12, 7, Palette.Green, "Chicken!");

// Add this image as second frame of animation
await pixoo.SendBufferAsync(1, imageFrame, 1, 2, 1000);

// let it play for 10 seconds
Thread.Sleep(10000);

await pixoo.SendResetGif();

// Jump back to my favorite clock (the one that rotates through other clocks)
await pixoo.SelectClock(3); // back to my favorite clock

// You could add default text items. Not really tested much.
//var itemList = new List<DisplayItem>();
//var item = new DisplayItem()
//{

//    TextId = 1,
//    type = (int)DisplayItemTypes.WEATHER_WORD,
//    x = 20,
//    y = 20,
//    TextWidth = 32,
//    TextHeight = 16,
//    speed = 100,
//    align = 1,
//};
//itemList.Add(item);
//await pixoo.DisplayCommand(itemList);
//Thread.Sleep(1000);


