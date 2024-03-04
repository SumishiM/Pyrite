using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public struct AxisBinding
    {
        public readonly InputSource Source = InputSource.None;

        public readonly ButtonBinding? Single;
        public readonly ButtonBinding Up;
        public readonly ButtonBinding Down;
        public readonly ButtonBinding Left;
        public readonly ButtonBinding Right;

        private Guid _deviceID = Guid.Empty;
        public Guid DeviceID
        {
            readonly get => _deviceID;
            internal set
            {
                if (Input.Devices.TryGetValue(value, out var device))
                {
                    if (Source == InputSource.Keyboard && device is not IKeyboard
                        || Source == InputSource.Mouse && device is not IMouse
                        || Source == InputSource.Gamepad && device is not IGamepad)
                        throw new InvalidDataException($"Device ID refereced is a {device.GetType().Name} but we expected a I{Source}.");
                    _deviceID = value;
                }
                throw new InvalidDataException($"No device registered at the given ID.");
            }
        }

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


        public readonly Vector2 GetAxis()
        {
            if (Input.Devices[DeviceID] is IGamepad gamepad)
            {
                if (Single is not null)
                    return Single.Value.GetAxis();
                return FromDPad(Up, Down, Left, Right);
            }

            return Vector2.Zero;
        }

        private static Vector2 FromDPad(
            ButtonBinding up, 
            ButtonBinding down, 
            ButtonBinding left, 
            ButtonBinding right)
        {
            int x = right.IsPressed() ? 1 : 0;
            int y = down.IsPressed() ? 1 : 0;
            x -= left.IsPressed() ? 1 : 0;
            y -= up.IsPressed() ? 1 : 0;

            return new(x, y);
        }
    }
}
