using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public class Input
    {
        public static Point ScreenMousePosition { get; internal set; } = new Point(0, 0);
        public static Point WorldMousePosition { get; internal set; } = new Point(0, 0);

        public static Dictionary<Guid, IInputDevice> Devices { get; internal set; } = [];
        public static List<IKeyboard> Keyboards { get; internal set; } = [];
        public static List<IGamepad> Gamepads { get; internal set; } = [];
        public static List<IMouse> Mice { get; internal set; } = [];


        public static void Initialize(IInputContext context)
        {
            foreach (var keyboard in context.Keyboards)
            {
                StartListeningToKeyboard(keyboard);
            }

            foreach (var mouse in context.Mice)
            {
                StartListeningToMouse(mouse);
            }

            foreach (var gamepad in context.Gamepads)
            {
                StartListeningToGamepad(gamepad);
            }

            context.ConnectionChanged += HandleConnectionChanged;
        }

        /// <summary>
        /// Handle device connection and disconnection
        /// </summary>
        private static void HandleConnectionChanged(IInputDevice device, bool connect /*true on connect*/)
        {
            if (device is IGamepad gamepad)
            {
                switch (connect)
                {
                    case true: StartListeningToGamepad(gamepad); break;
                    case false: StopListeningToGamepad(gamepad); break;
                }
            }
            else if (device is IKeyboard keyboard)
            {
                switch (connect)
                {
                    case true: StartListeningToKeyboard(keyboard); break;
                    case false: StopListeningToKeyboard(keyboard); break;
                }
            }
            else if (device is IMouse mouse)
            {
                switch (connect)
                {
                    case true: StartListeningToMouse(mouse); break;
                    case false: StopListeningToMouse(mouse); break;
                }
            }
            else
            {
                // other device
                throw new Exception($"Input device {device.Name} not handled.");
            }
        }

        #region Event collection
        private static void StartListeningToGamepad(IGamepad gamepad)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(gamepad.Index + " -> " + id);
            Input.Devices.Add(id, gamepad);
            Input.Gamepads.Add(gamepad);
        }
        private static void StopListeningToGamepad(IGamepad gamepad)
        {
            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == gamepad).Key);
            Input.Gamepads.Remove(gamepad);
        }
        private static void StartListeningToKeyboard(IKeyboard keyboard)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(keyboard.Index + " -> " + id);
            Input.Devices.Add(id, keyboard);
            Input.Keyboards.Add(keyboard);
        }
        private static void StopListeningToKeyboard(IKeyboard keyboard)
        {
            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == keyboard).Key);
            Input.Keyboards.Remove(keyboard);
        }
        private static void StartListeningToMouse(IMouse mouse)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(mouse.Index + " -> " + id);
            Input.Devices.Add(id, mouse);
            Input.Mice.Add(mouse);
        }
        private static void StopListeningToMouse(IMouse mouse)
        {
            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == mouse).Key);
            Input.Mice.Remove(mouse);
        }
        #endregion
    }


}
