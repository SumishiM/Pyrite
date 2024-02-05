using DiscordRPC;
using DiscordRPC.Logging;

namespace Pyrite.Utils.Discord
{
    internal class DiscordRichPresence
    {
        private static DiscordRpcClient? _client;
        public static DiscordRpcClient Client
        {
            get
            {
                if (_client == null)
                {
                    Initialize();
                    return _client!;
                }
                return _client;
            }
        }

        public static string ApplicationID = "";

        public static RichPresence DefaultPresence = new()
        {
            Timestamps = Timestamps.Now,
        };

        public static void Initialize()
        {
            _client = new(ApplicationID)
            {
                Logger = new ConsoleLogger() { Level = LogLevel.Warning }
            };

            _client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            _client.Initialize();

            _client.SetPresence(DefaultPresence);
        }
    }
}
