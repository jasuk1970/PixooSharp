# PixooSharp

This is a fork of https://github.com/jasuk1970/PixooSharp
Which is a nice setup for an usable API Interface.

I added some features and make the 'picture' more of an managable object.

## Usage
This library allows you to draw on a 64x64 canvas, then allows you to push that canvas to the Pixoo64 device.

### Initialise 
```c#
var pixoo = new Pixoo64("{Your Pixoo64 IP Address}", PixooSize.P64, true);
```

## New ImageFrame
```c#
var imageFrame = new ImageFrame(PixooSize.Pix64);
```

### Palette
This is a helper for some common colours, currently
Red, Black, White, Green, Yellow, Blue

This can be used anywhere that Rrb is used below.

### DrawLine
```c#
imageFrame.DrawLine(startX, startY, endX, endY, Rgb colour);
```

### Clear
This will fill the canvas with black.
```c#
imageFrame.Clear();
```

### Fill
This will fill the screen with a selected colour.
```c#
imageFrame.Fill(Rgb colour)
```

### DrawFilledRectangle
This will draw a solid rectangle on the canvas
```c#
imageFrame.DrawFilledRectangle(topX, topY, bottomX, bottomY, Rgb colour);
```

### DrawText
This will draw text on the canvas using the PICO8 3x5 font.
```c#
imageFrame.DrawText(x, y, Rgb colour, "{Your Text}");
```

### DrawPixel
```c#
imageFrame.DrawPixel(x, y, Rgb colour);
```
### Submit the image to the Pixoo
```c#
wait pixoo.SendBufferAsync(1, imageFrame, 0, 1, 0);
```

### SendResetGif
This will reset an animated GIF so that all frame ids are now usable again.
```c#
pixoo.SendResetGif();
```

### SendBufferAsync
This will need to be prefixed by the await command.

This sends the current canvas to the Pixoo64 device with a specified frame number.
```c#
await pixoo.SendBufferAsync(frameNumber);
```

### SendTextAsync
This will need to be prefixed with the await command.

This will display text non-destructively over the current canvas you have pushed to the Pixoo64 device and scroll the text in the direction specified in the command. It will keep scrolling until you tell it to stop.

```c#
await pixoo.SendTextAsync(x, y, Direction.RIGHT, "{Your Text}", Rgb colour);
```

### SendClearTextAsync
This will need to be prefixed with the await command.

This will clear any scrolling text on the screen.
```c#
await pixoo.SendClearTextAsync();
```

## Example usages
```c#
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
```

## Special Thanks
Special thanks go to [SomethingWithComputers](https://github.com/SomethingWithComputers/pixoo) the creator of a Pixoo Python library which I have used as the basis to create this .net version.
Also special thanks go to the creator of the [PICO8](https://www.lexaloffle.com/pico-8.php) 3x5 font which I have used (Converted from SomethingWithComputers project)


And also Special Thanks to [jasuk1970](https://github.com/jasuk1970/PixooSharp)

Art sources
https://opengameart.org/
https://opengameart.org/content/golden-ui
https://opengameart.org/content/10-fantasy-rpg-enemies
