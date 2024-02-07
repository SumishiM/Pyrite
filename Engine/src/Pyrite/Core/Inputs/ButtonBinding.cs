using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public struct ButtonBinding
    {
        public readonly InputSource Source = InputSource.None;

        private readonly GamepadButtons? _gamepad;
        private readonly Key? _keyboard;
        private readonly GamepadAxis? _axis;
        private readonly MouseButton? _mouse;

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

        public ButtonBinding() { }
        public ButtonBinding(Key key)
        {
            Source = InputSource.Keyboard;
            _keyboard = key;
        }

        public ButtonBinding(GamepadButtons button)
        {
            Source = InputSource.Gamepad;
            _gamepad = button;
        }

        public ButtonBinding(MouseButton button)
        {
            Source = InputSource.Mouse;
            _mouse = button;
        }

        public ButtonBinding(GamepadAxis axis)
        {
            Source = InputSource.GamepadAxis;
            _axis = axis;
        }

        public readonly bool IsPressed()
        {
            var device = Input.Devices[DeviceID];

            return Source switch
            {
                InputSource.Keyboard => ((IKeyboard)device).IsKeyPressed(_keyboard!.Value),
                InputSource.Mouse => ((IMouse)device).IsButtonPressed(_mouse!.Value),
                InputSource.Gamepad => _gamepad!.Value switch
                {
                    GamepadButtons.RightTrigger => ((IGamepad)device).Triggers[0].Position > 0f,
                    GamepadButtons.LeftTrigger => ((IGamepad)device).Triggers[1].Position > 0f,
                    _ => ((IGamepad)device).Buttons[(int)_gamepad!.Value].Pressed,
                },
                _ => false,
            };
        }

        public readonly Vector2 GetAxis()
        {
            if (Source != InputSource.GamepadAxis)
                return Vector2.Zero;

            if (Input.Devices[DeviceID] is IGamepad gamepad)
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
