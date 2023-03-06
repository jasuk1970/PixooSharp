# PixooSharp

Art source
https://opengameart.org/
https://opengameart.org/content/golden-ui
https://opengameart.org/content/10-fantasy-rpg-enemies

I've been looking to find a way of talking to the Pixoo API in .net but didn't find anything usable so I thought I'd have a go at putting one together myself. I took inspiration from a python library to get me going. (See special thanks below).

The project is in its early stages and may have a few bugs.

The repository consists of a class library that contains all the code to interface with the Pixoo 64 API plus a console application to do some tests.

## Usage
This library allows you to draw on a 64x64 canvas, then allows you to push that canvas to the Pixoo64 device.

### Initialise 
```c#
var pixoo = new Pixoo64("{Your Pixoo64 IP Address}", 64, true);
```

### Palette
This is a helper for some common colours, currently
* Red
* Black
* White
* Green

This can be used anywhere that Rrb is used below.

### DrawLine
```c#
pixoo.DrawLine(startX, startY, endX, endY, Rgb colour);
```

### Clear
This will fill the canvas with black.
```c#
pixoo.Clear();
```

### Fill
This will fill the screen with a selected colour.
```c#
pixoo.Fill(Rgb colour)
```

### DrawFilledRectangle
This will draw a solid rectangle on the canvas
```c#
pixoo.DrawFilledRectangle(topX, topY, bottomX, bottomY, Rgb colour);
```

### DrawText
This will draw text on the canvas using the PICO8 3x5 font.
```c#
pixoo.DrawText(x, y, Rgb colour, "{Your Text}");
```

### DrawPixel
```c#
pixoo.DrawPixel(x, y, Rgb colour);
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
var pixoo = new Pixoo64("172.24.1.241", 64, true);
pixoo.DrawLine(0, 0, 31, 0, Palette.White);
pixoo.DrawLine(31, 0, 31, 31, Palette.White);
pixoo.DrawLine(31, 31, 0, 31, Palette.White);
pixoo.DrawLine(0, 31, 0, 0, Palette.White);
pixoo.DrawText(0, 34, Palette.Green, "Hello World!");
pixoo.DrawFilledRectangle(10, 10, 20, 20, Palette.Red);
// We are not creating a gif to animate so reset to make sure the image Id will work
await pixoo.SendResetGif();
// We now specify the frame Id as part of the SendBuffer
await pixoo.SendBufferAsync(0);
await pixoo.SendTextAsync(0, 0, Direction.RIGHT, "This is a message that scrolls across the screen. I hope it works!", Palette.White);
Thread.Sleep(20000);
await pixoo.SendClearTextAsync();
```

## Special Thanks
Special thanks go to [SomethingWithComputers](https://github.com/SomethingWithComputers/pixoo) the creator of a Pixoo Python library which I have used as the basis to create this .net version.

Also special thanks go to the creator of the [PICO8](https://www.lexaloffle.com/pico-8.php) 3x5 font which I have used (Converted from SomethingWithComputers project)
