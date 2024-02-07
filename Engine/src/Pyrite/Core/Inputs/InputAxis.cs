using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct InputAxis
    {
        public readonly InputButton? Single;
        public readonly InputButton Up;
        public readonly InputButton Left;
        public readonly InputButton Down;
        public readonly InputButton Right;
        public readonly InputSource Source;

        public InputAxis(InputButton up, InputButton left, InputButton down, InputButton right)
        {
            Source = up.Source;
            Up = up;
            Left = left;
            Down = down;
            Right = right;
        }
        public InputAxis(Key up, Key left, Key down, Key right)
        {
            Source = InputSource.Keyboard;
            Up = new(up);
            Left = new(left);
            Down = new(down);
            Right = new(right);
        }

        public InputAxis(GamepadButtons up, GamepadButtons left, GamepadButtons down, GamepadButtons right)
        {
            Source = InputSource.Gamepad;
            Up = new(up);
            Left = new(left);
            Down = new(down);
            Right = new(right);
        }

        public InputAxis(GamepadAxis axis)
        {
            Source = InputSource.Gamepad;
            Single = new(axis);
        }

        public override string ToString()
        {
            if (Single != null)
                return Single.Value.ToString();

            var buttons = new string[] { Up.ToString(), Left.ToString(), Down.ToString(), Right.ToString() };
            return String.Join(", ", buttons);
        }

        public Vector2 Check(InputState state)
        {
            if (Single != null)
                return Single.Value.GetAxis(state.Gamepad);

            int x = Right.Check(state) ? 1 : 0;
            int y = Down.Check(state) ? 1 : 0;
            x -= Left.Check(state) ? 1 : 0;
            y -= Up.Check(state) ? 1 : 0;

            return new(x, y);
        }
    }
}
