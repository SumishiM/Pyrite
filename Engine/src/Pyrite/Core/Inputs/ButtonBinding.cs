using Microsoft.Xna.Framework.Input;
using Pyrite.Core.Geometry;

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

        public readonly bool IsPressed(InputState state)
        {
            return Source switch
            {
                InputSource.Keyboard => state.Keyboard.IsKeyDown(_keyboard!.Value.ToXna()),
                InputSource.Gamepad => state.Gamepad.IsButtonDown(_gamepad!.Value.ToXna()),
                InputSource.Mouse => _mouse!.Value switch
                {
                    MouseButtons.Left => state.Mouse.LeftButton == ButtonState.Pressed,
                    MouseButtons.Right => state.Mouse.RightButton == ButtonState.Pressed,
                    MouseButtons.Middle => state.Mouse.MiddleButton == ButtonState.Pressed,
                    MouseButtons.Button1 => state.Mouse.XButton1 == ButtonState.Pressed,
                    MouseButtons.Button2 => state.Mouse.XButton2 == ButtonState.Pressed,
                    _ => false,
                },
                _ => false,
            };
        }

        public readonly Vector2 GetAxis(InputState state)
        {
            if (Source != InputSource.GamepadAxis)
                return Vector2.Zero;

            return _axis switch
            {
                GamepadAxis.LeftThumbstick => new(state.Gamepad.ThumbSticks.Left.X, -state.Gamepad.ThumbSticks.Left.Y),
                GamepadAxis.RightThumbstick => new(state.Gamepad.ThumbSticks.Right.X, -state.Gamepad.ThumbSticks.Right.Y),
                GamepadAxis.Dpad => FromDPad(state.Gamepad.DPad),
                _ => Vector2.Zero,
            };
        }

        private static Vector2 FromDPad(GamePadDPad DPad)
        {
            int x = DPad.Right == ButtonState.Pressed ? 1 : 0;
            int y = DPad.Down == ButtonState.Pressed ? 1 : 0;
            x -= DPad.Left == ButtonState.Pressed ? 1 : 0;
            y -= DPad.Up == ButtonState.Pressed ? 1 : 0;

            return new(x, y);
        }
    }
}
