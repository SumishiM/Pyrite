using Silk.NET.Input;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;

namespace Pyrite.Core.Inputs
{
    public class InputContextMapping
    {
        public event Action? OnConnectionChanged;


        public InputContextMapping(IInputContext context) 
        {
            foreach (var gamepad in context.Gamepads)
            {
                gamepad.ButtonDown += GamepadButtonDown;
                gamepad.ButtonUp += GamepadButtonUp;
                gamepad.ThumbstickMoved += GamepadThumbstickMoved;
                gamepad.TriggerMoved += GamepadTriggerMoved;
            }

            foreach (var joystick in context.Joysticks)
            {
                joystick.ButtonDown += JoystickButtonDown;
                joystick.ButtonUp += JoystickButtonUp;
                joystick.AxisMoved += JoystickAxisMoved;
                joystick.HatMoved += JoystickHatMoved;
            }

            foreach (var keyboard in context.Keyboards)
            {
                keyboard.KeyDown += KeyDown;
                keyboard.KeyUp += KeyUp;
            }

            foreach (var mouse in context.Mice)
            {
                mouse.MouseMove += MouseMoved;
                mouse.Scroll += MouseScroll;
                mouse.Click += MouseClick;
                mouse.DoubleClick += MouseDoubleClick;
                mouse.MouseDown += MouseButtonDown;
                mouse.MouseUp += MouseButtonUp;
            }
            
            context.ConnectionChanged += HandleConnectionChanged;

        }

        private void HandleConnectionChanged(IInputDevice device, bool arg2)
        {
            if (device is IGamepad gamepad)
            {

            }
            else if (device is IKeyboard keyboard) { }
            else if (device is IMouse mouse) { }
            else
            {
                // other device
            }
        }

        #region Mice
        private void MouseButtonUp(IMouse mouse, Silk.NET.Input.MouseButton button)
        {
            Console.WriteLine($"Inputs [{nameof(MouseButtonUp)}]: {button}");
        }

        private void MouseButtonDown(IMouse mouse, Silk.NET.Input.MouseButton button)
        {
            Console.WriteLine($"Inputs [{nameof(MouseButtonDown)}]: {button}");
        }

        private void MouseDoubleClick(IMouse mouse, Silk.NET.Input.MouseButton button, System.Numerics.Vector2 vector)
        {
            Console.WriteLine($"Inputs [{nameof(MouseDoubleClick)}]: {button} At:{vector}");
        }

        private void MouseClick(IMouse mouse, Silk.NET.Input.MouseButton button, System.Numerics.Vector2 vector)
        {
            Console.WriteLine($"Inputs [{nameof(MouseClick)}]: {button} At:{vector}");
        }

        private void MouseScroll(IMouse mouse, ScrollWheel wheel)
        {
            Console.WriteLine($"Inputs [{nameof(MouseScroll)}]: X:{wheel.X} Y:{wheel.Y}");
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
            Console.WriteLine($"Inputs [{nameof(KeyUp)}]: {key} Arg:{arg3}");
        }

        private void KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            Console.WriteLine($"Inputs [{nameof(KeyDown)}]: {key} Arg:{arg3}");
        }
        #endregion


        #region Joysticks
        private void JoystickHatMoved(IJoystick joystick, Hat hat)
        {
            Console.WriteLine($"Inputs [{nameof(JoystickHatMoved)}]: {hat.Index} At:{hat.Position}");
        }

        private void JoystickAxisMoved(IJoystick joystick, Axis axis)
        {
            Console.WriteLine($"Inputs [{nameof(JoystickAxisMoved)}]: {axis} At:{axis.Position}");
        }

        private void JoystickButtonUp(IJoystick joystick, Button button)
        {
            Console.WriteLine($"Inputs [{nameof(JoystickButtonUp)}]: {button}");
        }

        private void JoystickButtonDown(IJoystick joystick, Button button)
        {
            Console.WriteLine($"Inputs [{nameof(JoystickButtonDown)}]: {button}");
        }
        #endregion

        #region Gamepads
        private void GamepadTriggerMoved(IGamepad gamepad, Trigger trigger)
        {
            Console.WriteLine($"Inputs [{nameof(GamepadTriggerMoved)}]: {trigger}");
        }

        private void GamepadThumbstickMoved(IGamepad gamepad, Thumbstick thumbstick)
        {
            Console.WriteLine($"Inputs [{nameof(GamepadThumbstickMoved)}]: {thumbstick}");
        }

        private void GamepadButtonUp(IGamepad gamepad, Button button)
        {
            Console.WriteLine($"Inputs [{nameof(GamepadButtonUp)}]: {button.}");
        }

        private void GamepadButtonDown(IGamepad gamepad, Button button)
        {
            Console.WriteLine($"Inputs [{nameof(GamepadButtonDown)}]: {button}");
        }
        #endregion
    }
}
