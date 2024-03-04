using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct ButtonBinding
    {
        public readonly InputSource Source = InputSource.None;

        private readonly GamepadButtons? _gamepad;
        private readonly Keys? _keyboard;
        private readonly GamepadAxis? _axis;
        private readonly MouseButtons? _mouse;

        public ButtonBinding() { }
        public ButtonBinding(Keys key)
        {
            Source = InputSource.Keyboard;
            _keyboard = key;
        }

        public ButtonBinding(GamepadButtons button)
        {
            Source = InputSource.Gamepad;
            _gamepad = button;
        }

        public ButtonBinding(MouseButtons button)
        {
            Source = InputSource.Mouse;
            _mouse = button;
        }

        public ButtonBinding(GamepadAxis axis)
        {
            Source = InputSource.GamepadAxis;
            _axis = axis;
        }

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>Whether the binding listen to the key.</returns>
        internal readonly bool ListenToInput(Keys key) => Source == InputSource.Keyboard && _keyboard == key;

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="button">button to check.</param>
        /// <returns>Whether the binding listen to the button.</returns>
        internal readonly bool ListenToInput(GamepadButtons button) => Source == InputSource.Gamepad && _gamepad == button;

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="axis">axis to check.</param>
        /// <returns>Whether the binding listen to the axis.</returns>
        internal readonly bool ListenToInput(GamepadAxis axis) => Source == InputSource.GamepadAxis && _axis == axis;

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="button">button to check.</param>
        /// <returns>Whether the binding listen to the button.</returns>
        internal readonly bool ListenToInput(MouseButtons button) => Source == InputSource.Mouse && _mouse == button;

        public readonly bool IsPressed(IInputDevice device)
        {
            return Source switch
            {
                InputSource.Keyboard => ((IKeyboard)device).IsKeyPressed((Key)_keyboard!.Value),
                InputSource.Mouse => ((IMouse)device).IsButtonPressed((MouseButton)_mouse!.Value),
                InputSource.Gamepad => _gamepad!.Value switch
                {
                    GamepadButtons.RightTrigger => ((IGamepad)device).Triggers[0].Position > 0f,
                    GamepadButtons.LeftTrigger => ((IGamepad)device).Triggers[1].Position > 0f,
                    _ => ((IGamepad)device).Buttons[(int)_gamepad!.Value].Pressed,
                },
                _ => false,
            };
        }

        public readonly Vector2 GetAxis(IInputDevice device)
        {
            if (Source != InputSource.GamepadAxis)
                return Vector2.Zero;

            if (device is IGamepad gamepad)
            {
                return _axis switch
                {
                    GamepadAxis.LeftThumbstick => new(gamepad.LeftThumbstick().X, gamepad.LeftThumbstick().Y),
                    GamepadAxis.RightThumbstick => new(gamepad.RightThumbstick().X, gamepad.RightThumbstick().Y),
                    GamepadAxis.Dpad => FromDPad(
                        gamepad.DPadUp().Pressed, 
                        gamepad.DPadDown().Pressed, 
                        gamepad.DPadLeft().Pressed, 
                        gamepad.DPadRight().Pressed),
                    _ => Vector2.Zero,
                };
            }

            return Vector2.Zero;
        }

        private static Vector2 FromDPad(bool up, bool down, bool left, bool right)
        {
            int x = right ? 1 : 0;
            int y = down ? 1 : 0;
            x -= left ? 1 : 0;
            y -= up ? 1 : 0;

            return new(x, y);
        }
    }
}
