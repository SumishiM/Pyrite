using Silk.NET.Input;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using System.Runtime.CompilerServices;

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
        LeftThumb,
        RightThumb, 
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

    public struct RawInputCallback
    {
        public InputSource Source;

        private Key? _keyboard;
        private MouseButton? _mouse;
        private GamepadButtons? _gamepad;
        private GamepadAxis? _axis;

        private Vector2 _axisValue;
        private bool _pressed;
    }

    public struct KeyboardState
    {
        private readonly HashSet<Key> KeyPressed;
        public readonly bool IsKeyDown(Key key) => KeyPressed.Contains(key);
        public readonly bool IsKeyUp(Key key) => !KeyPressed.Contains(key);
    }

    public struct GamepadState
    {
        private readonly HashSet<GamepadButtons> ButtonPressed;

        public Vector2 LeftThumbstrick { get; internal set; }
        public Vector2 RightThumbstrick { get; internal set; }
        public Vector2 Dpad { get; internal set; }

        public readonly bool IsButtonDown(GamepadButtons button) => ButtonPressed.Contains(button);
        public readonly bool IsButtonUp(GamepadButtons button) => !ButtonPressed.Contains(button);
    }

    public struct MouseState
    {
        private readonly HashSet<MouseButton> ButtonPressed;

        public Vector2 Wheel { get; internal set; }
        public Vector2 DeltaPosition { get; internal set; }
        public readonly bool IsButtonDown(MouseButton button) => ButtonPressed.Contains(button);
        public readonly bool IsButtonUp(MouseButton button) => !ButtonPressed.Contains(button);
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

        public List<IInputDevice> Devices { get; private set; }

         

        public InputContextMapping(IInputContext context)
        {
            Devices = [];
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
            Devices.Add(gamepad);
        }
        private void StopListeningToGamepad(IGamepad gamepad)
        {
            gamepad.ButtonDown -= GamepadButtonDown;
            gamepad.ButtonUp -= GamepadButtonUp;
            gamepad.ThumbstickMoved -= GamepadThumbstickMoved;
            gamepad.TriggerMoved -= GamepadTriggerMoved;
            Devices.Remove(gamepad);
        }
        private void StartListeningToKeyboard(IKeyboard keyboard)
        {
            keyboard.KeyDown += KeyDown;
            keyboard.KeyUp += KeyUp;
            Devices.Add(keyboard);
        }
        private void StopListeningToKeyboard(IKeyboard keyboard)
        {
            keyboard.KeyDown -= KeyDown;
            keyboard.KeyUp -= KeyUp;
            Devices.Remove(keyboard);
        }
        private void StartListeningToMouse(IMouse mouse)
        {
            mouse.MouseMove += MouseMoved;
            mouse.Scroll += MouseScroll;
            mouse.Click += MouseClick;
            mouse.DoubleClick += MouseDoubleClick;
            mouse.MouseDown += MouseButtonDown;
            mouse.MouseUp += MouseButtonUp;
            Devices.Add(mouse);
        }
        private void StopListeningToMouse(IMouse mouse)
        {
            mouse.MouseMove -= MouseMoved;
            mouse.Scroll -= MouseScroll;
            mouse.Click -= MouseClick;
            mouse.DoubleClick -= MouseDoubleClick;
            mouse.MouseDown -= MouseButtonDown;
            mouse.MouseUp -= MouseButtonUp;
            Devices.Remove(mouse);
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
            Input.ScreenMousePosition = new(vector.X, vector.Y);
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
            Console.WriteLine(button.Name + ", " + button.Index);
        }
        #endregion
    }
}
