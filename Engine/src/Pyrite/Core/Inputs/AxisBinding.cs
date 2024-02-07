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
        public AxisBinding(ButtonBinding up, ButtonBinding left, ButtonBinding down, ButtonBinding right)
        {
            Source = up.Source;
            Up = up;
            Left = left;
            Down = down;
            Right = right;
        }
        public AxisBinding(Key up, Key left, Key down, Key right)
        {
            Source = InputSource.Keyboard;
            Up = new(up);
            Left = new(left);
            Down = new(down);
            Right = new(right);
        }

        public AxisBinding(GamepadButtons up, GamepadButtons left, GamepadButtons down, GamepadButtons right)
        {
            Source = InputSource.Gamepad;
            Up = new(up);
            Left = new(left);
            Down = new(down);
            Right = new(right);
        }

        public AxisBinding(GamepadAxis axis)
        {
            Source = InputSource.Gamepad;
            Single = new(axis);
        }

    }
}
