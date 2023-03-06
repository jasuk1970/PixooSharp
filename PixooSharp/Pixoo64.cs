using PixooSharp.Assets;
using PixooSharp.Commands;
using System.Text;
using System.Text.Json;

namespace PixooSharp
{
    public class Pixoo64
    {
        private readonly string _url;
        private readonly bool _debug;
        private int _pushCount = 1;
        private int _textId = 0;

        public Pixoo64(string address, bool debug = false, int pushCount = 50)
        {
            _url = string.Format("http://{0}/post", address);
            _debug = debug;
            _pushCount = pushCount;
            // Set default boot colour to black
        }
        public async Task SendBufferAsync(int picId, ImageFrame imageFrame, int frameId = 0, int frameCount = 1, int speed = 0)
        {
            // generate pixoo format image
   
            var command = new SendBufferCommand(imageFrame.ScreenSize, picId, frameId, frameCount, speed, imageFrame.Base64Buffer);
            var jsonString = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonString);
        }

        public async Task SendTextAsync(int x, int y, Direction direction, string text, Rgb colour, int font = 2, int textWidth = 64, int textSpeed = 0)
        {
            var command = new SendTextCommand(_textId, x, y, direction, text, colour, font, textWidth, textSpeed);
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }

        public async Task SendClearTextAsync()
        {
            var command = new SendClearTextCommand();
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }

        public async Task SendNoise(bool sounding)
        {
            var command = new SendNoiseCommand(sounding);
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }

        public async Task SendResetGif()
        {
            var command = new SendResetGifCommand();
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }
        public async Task SendGetDailList()
        {
            var command = new GetDialList();
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand, "https://app.divoom-gz.com/Channel/GetDialList");

        }
        public async Task SelectClock(int clockId)
        {
            var command = new SendSelectClock(clockId);
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }
        public async Task DisplayCommand(List<DisplayItem> itemList)
        {
            var command = new SendDisplayCommand(itemList);
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }
        public async Task PlayBuzzer(int duration, int onTime, int offTime)
        {
            var command = new SendPlayBuzzer(duration, onTime, offTime);
            var jsonCommand = JsonSerializer.Serialize(command);
            await SendCommandAsync(jsonCommand);
        }
        
        public async Task SendCommandAsync(string jsonPayload, string url = "")
        {
            var postUrl = _url;
            if (_debug)
            {
                Console.WriteLine(jsonPayload);
            }
            if(url != "")
            {
                postUrl = url;
            }
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                var response = await httpClient.PostAsync(postUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    _pushCount++;
                    Console.WriteLine(result.Result);
                }
                else
                {
                    // Something went wrong
                    Console.WriteLine("error!");
                }
            }
        }
    }
}
