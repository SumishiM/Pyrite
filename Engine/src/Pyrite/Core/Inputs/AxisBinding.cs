using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct AxisBinding
    {
        public readonly InputSource Source = InputSource.None;

        public readonly ButtonBinding? Single;
        public readonly ButtonBinding Up;
        public readonly ButtonBinding Down;
        public readonly ButtonBinding Left;
        public readonly ButtonBinding Right;

        public AxisBinding() { }
        public AxisBinding(ButtonBinding up, ButtonBinding down, ButtonBinding left, ButtonBinding right)
        {
            Source = up.Source;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
        public AxisBinding(Keys up, Keys down, Keys left, Keys right)
        {
            Source = InputSource.Keyboard;
            Up = new(up);
            Down = new(down);
            Left = new(left);
            Right = new(right);
        }

        public AxisBinding(GamepadButtons up, GamepadButtons down, GamepadButtons left, GamepadButtons right)
        {
            Source = InputSource.Gamepad;
            Up = new(up);
            Down = new(down);
            Left = new(left);
            Right = new(right);
        }

        public AxisBinding(GamepadAxis axis)
        {
            Source = InputSource.Gamepad;
            Single = new(axis);
        }

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>Whether the binding listen to the key.</returns>
        internal readonly bool ListenToInput(Keys key)
            => Source == InputSource.Keyboard
            && Up.ListenToInput(key) || Down.ListenToInput(key) || Left.ListenToInput(key) || Right.ListenToInput(key);

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="button">Button to check.</param>
        /// <returns>Whether the binding listen to the button.</returns>
        internal readonly bool ListenToInput(GamepadButtons button)
            => Source == InputSource.Gamepad
            && Up.ListenToInput(button) || Down.ListenToInput(button) || Left.ListenToInput(button) || Right.ListenToInput(button);

        /// <summary>
        /// Whether the binding is listening to a given input.
        /// </summary>
        /// <param name="axis">Axis to check.</param>
        /// <returns>Whether the binding listen to the axis.</returns>
        internal readonly bool ListenToInput(GamepadAxis axis)
            => Source == InputSource.GamepadAxis && Single!.Value.ListenToInput(axis);

        /// <summary>
        /// Whether the binding is listening to every inputs.
        /// </summary>
        /// <returns>Whether the binding listen every inputs.</returns>
        internal readonly bool ListenToAllInput(Keys up, Keys down, Keys left, Keys right)
            => Source == InputSource.Keyboard
            && Up.ListenToInput(up) && Down.ListenToInput(down) && Left.ListenToInput(left) && Right.ListenToInput(right);

        /// <summary>
        /// Whether the binding is listening to every inputs.
        /// </summary>
        /// <returns>Whether the binding listen every inputs.</returns>
        internal readonly bool ListenToAllInput(GamepadButtons up, GamepadButtons down, GamepadButtons left, GamepadButtons right)
            => Source == InputSource.Keyboard
            && Up.ListenToInput(up) && Down.ListenToInput(down) && Left.ListenToInput(left) && Right.ListenToInput(right);

        public readonly Vector2 GetAxis(IInputDevice device)
        {
            if (device is IGamepad gamepad)
            {
                if (Single is not null)
                    return Single.Value.GetAxis(gamepad);
                return FromDPad(
                    Up.IsPressed(gamepad), 
                    Down.IsPressed(gamepad), 
                    Left.IsPressed(gamepad), 
                    Right.IsPressed(gamepad));
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
