using Silk.NET.Input;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using System.Drawing;
using System.Reflection;

namespace Pyrite.Core.Inputs
{
    public enum InputSource
    {
        None = -1,
        Keyboard = 0,
        Mouse = 1,
        Gamepad = 2,
        GamepadAxis = 3,
    }

    public enum GamepadAxis
    {
        LeftThumbstick,
        RightThumbstick,
        Dpad
    }

    public enum GamepadButtons
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3,
        LeftBumper = 4,
        RightBumper = 5,
        Back = 6,
        Start = 7,
        Home = 8,
        LeftThumbstick = 9,
        RightThumbstick = 10,
        DpadUp = 11,
        DpadRight = 12,
        DpadDown = 13,
        DpadLeft = 14,
        LeftTrigger = 15,
        RightTrigger = 16,
    }

    public struct KeyboardState(IKeyboard keyboard)
    {
        private readonly IKeyboard _keyboard = keyboard;
        private readonly HashSet<Key> KeyPressed;
        public readonly bool IsKeyDown(Key key) => KeyPressed.Contains(key);
        public readonly bool IsKeyUp(Key key) => !KeyPressed.Contains(key);
    }

    public struct GamepadState(IGamepad gamepad)
    {
        private readonly IGamepad _gamepad = gamepad;

        /// <summary>
        /// Left thumbstick direction value
        /// </summary>
        public readonly Vector2 LeftThumbstrick => new(_gamepad.LeftThumbstick().X, _gamepad.LeftThumbstick().Y);

        /// <summary>
        /// Right thumbstick direction value
        /// </summary>
        public readonly Vector2 RightThumbstrick => new(_gamepad.RightThumbstick().X, _gamepad.RightThumbstick().Y);

        /// <summary>
        /// Dpad direction value
        /// </summary>
        public readonly Vector2 Dpad => FromDPad(
            _gamepad.DPadLeft().Pressed,
            _gamepad.DPadRight().Pressed,
            _gamepad.DPadUp().Pressed,
            _gamepad.DPadDown().Pressed);

        /// <summary>
        /// Create a vector2 from dpad
        /// </summary>
        private static Vector2 FromDPad(bool left, bool right, bool up, bool down)
        {
            int x = right ? 1 : 0;
            int y = down ? 1 : 0;
            x -= left ? 1 : 0;
            y -= up ? 1 : 0;

            return Vector2.Normalized(new(x, y));
        }

        public readonly bool IsButtonDown(GamepadButtons button)
        {
            if (button == GamepadButtons.LeftTrigger)
            {
                return _gamepad.Triggers[1].Position > 0f;
            }

            if (button == GamepadButtons.RightTrigger)
            {
                return _gamepad.Triggers[0].Position > 0f;
            }

            return _gamepad.Buttons[(int)button].Pressed;
        }

        public readonly bool IsButtonUp(GamepadButtons button) => !IsButtonDown(button);
    }

    public struct MouseState(IMouse mouse)
    {
        private readonly IMouse _mouse = mouse;

        public Vector2 Wheel { get; internal set; }
        public Vector2 DeltaPosition { get; internal set; }
        public readonly bool IsButtonDown(MouseButton button) => _mouse.IsButtonPressed(button);
        public readonly bool IsButtonUp(MouseButton button) => !_mouse.IsButtonPressed(button);
    }

    public readonly struct InputState(KeyboardState keyboard, MouseState mouse, GamepadState gamepad)
    {
        public readonly KeyboardState Keyboard = keyboard;
        public readonly MouseState Mouse = mouse;
        public readonly GamepadState Gamepad = gamepad;
    }


    public class InputContextMapping
    {
        public event Action? OnConnectionChanged;
        public static float ThumbsticksThreshold = 0.1f;

        private KeyboardState _keyboard;
        private MouseState _mouse;
        private GamepadState _gamepad;

        public InputContextMapping(IInputContext context)
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
        private void HandleConnectionChanged(IInputDevice device, bool connect /*true on connect*/)
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
        private void StartListeningToGamepad(IGamepad gamepad)
        {
            gamepad.ButtonDown += GamepadButtonDown;
            gamepad.ButtonUp += GamepadButtonUp;
            gamepad.ThumbstickMoved += GamepadThumbstickMoved;
            gamepad.TriggerMoved += GamepadTriggerMoved;

            var id = Guid.NewGuid();
            Console.WriteLine(gamepad.Index + " -> " + id);
            Input.Devices.Add(id, gamepad);
        }
        private void StopListeningToGamepad(IGamepad gamepad)
        {
            gamepad.ButtonDown -= GamepadButtonDown;
            gamepad.ButtonUp -= GamepadButtonUp;
            gamepad.ThumbstickMoved -= GamepadThumbstickMoved;
            gamepad.TriggerMoved -= GamepadTriggerMoved;

            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == gamepad).Key);
        }
        private void StartListeningToKeyboard(IKeyboard keyboard)
        {
            keyboard.KeyDown += KeyDown;
            keyboard.KeyUp += KeyUp;

            var id = Guid.NewGuid();
            Console.WriteLine(keyboard.Index + " -> " + id);
            Input.Devices.Add(id, keyboard);
        }
        private void StopListeningToKeyboard(IKeyboard keyboard)
        {
            keyboard.KeyDown -= KeyDown;
            keyboard.KeyUp -= KeyUp;

            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == keyboard).Key);
        }
        private void StartListeningToMouse(IMouse mouse)
        {
            mouse.MouseMove += MouseMoved;
            mouse.Scroll += MouseScroll;
            mouse.Click += MouseClick;
            mouse.DoubleClick += MouseDoubleClick;
            mouse.MouseDown += MouseButtonDown;
            mouse.MouseUp += MouseButtonUp;

            var id = Guid.NewGuid();
            Console.WriteLine(mouse.Index + " -> " + id);
            Input.Devices.Add(id, mouse);
        }
        private void StopListeningToMouse(IMouse mouse)
        {
            mouse.MouseMove -= MouseMoved;
            mouse.Scroll -= MouseScroll;
            mouse.Click -= MouseClick;
            mouse.DoubleClick -= MouseDoubleClick;
            mouse.MouseDown -= MouseButtonDown;
            mouse.MouseUp -= MouseButtonUp;

            Input.Devices.Remove(Input.Devices.First(kvp => kvp.Value == mouse).Key);
        }
        #endregion

        #region Mice
        private void MouseButtonUp(IMouse mouse, Silk.NET.Input.MouseButton button)
        {
        }

        private void MouseButtonDown(IMouse mouse, Silk.NET.Input.MouseButton button)
        {
        }

        private void MouseDoubleClick(IMouse mouse, Silk.NET.Input.MouseButton button, System.Numerics.Vector2 vector)
        {
        }

        private void MouseClick(IMouse mouse, Silk.NET.Input.MouseButton button, System.Numerics.Vector2 vector)
        {
        }

        private void MouseScroll(IMouse mouse, ScrollWheel wheel)
        {
        }

        private void MouseMoved(IMouse mouse, System.Numerics.Vector2 vector)
        {
            Input.ScreenMousePosition = mouse.Position;
            Input.WorldMousePosition = Camera.Main.ScreenToWorldPosition(Input.ScreenMousePosition);
        }
        #endregion

        #region Keyboards
        private void KeyUp(IKeyboard keyboard, Key key, int arg3)
        {
        }

        private void KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
        }
        #endregion

        #region Gamepads
        private void GamepadTriggerMoved(IGamepad gamepad, Trigger trigger)
        {
            Console.WriteLine(trigger.Position + ", " + trigger.Index);
            //Console.WriteLine((GamepadButtons)trigger.Index + ", " + _gamepad.IsButtonDown(trigger.Index == 1 ? GamepadButtons.LeftTrigger : GamepadButtons.RightTrigger));
            
        }

        private void GamepadThumbstickMoved(IGamepad gamepad, Thumbstick thumbstick)
        {
            if (new Vector2(thumbstick.X, thumbstick.Y).SquaredLength() < ThumbsticksThreshold * ThumbsticksThreshold)
                return;
        }

        private void GamepadButtonUp(IGamepad gamepad, Button button)
        {
        }

        private void GamepadButtonDown(IGamepad gamepad, Button button)
        {
            //Console.WriteLine((GamepadButtons)button.Index + ", " + _gamepad.IsButtonDown((GamepadButtons)button.Index));
        }
        #endregion
    }
}
