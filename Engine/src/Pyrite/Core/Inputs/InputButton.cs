using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{   
    public struct InputButton
    {
        public readonly InputSource Source = InputSource.None;

        private readonly GamepadButtons? _gamepad;
        private readonly Key? _keyboard;
        private readonly GamepadAxis? _axis;
        private readonly MouseButton? _mouse;

        public InputButton() { }
        public InputButton(Key key)
        {
            Source = InputSource.Keyboard;
            _keyboard = key;
        }

        public InputButton(GamepadButtons button)
        {
            Source = InputSource.Gamepad;
            _gamepad = button;
        }

        public InputButton(MouseButton button)
        {
            Source = InputSource.Mouse;
            _mouse = button;
        }
        public InputButton(GamepadAxis axis)
        {
            Source = InputSource.GamepadAxis;
            _axis = axis;
        }

        public bool Check(InputState state)
        {
            return Source switch
            {
                InputSource.None => false,
                InputSource.Keyboard => state.Keyboard.IsKeyDown(_keyboard!.Value),
                InputSource.Gamepad => state.Gamepad.IsButtonDown(_gamepad!.Value),
                InputSource.Mouse => state.Mouse.IsButtonDown(_mouse!.Value),
                _ => false,
            };
        }

        public Vector2 GetAxis(GamepadState gamepadState)
        {
            if (Source == InputSource.GamepadAxis)
            {
                return _axis!.Value switch
                {
                    GamepadAxis.LeftThumb => new(gamepadState.LeftThumbstrick.X, -gamepadState.LeftThumbstrick.Y),
                    GamepadAxis.RightThumb => new(gamepadState.RightThumbstrick.X, -gamepadState.RightThumbstrick.Y),
                    GamepadAxis.Dpad => ButtonToAxis(
                                                gamepadState.Dpad.Y > 0f,
                                                gamepadState.Dpad.X > 0f,
                                                gamepadState.Dpad.X < 0f,
                                                gamepadState.Dpad.Y < 0f),
                    _ => throw new Exception($"Gamepad axis '{_axis}' is not supported yet."),
                };
            }

            return Vector2.Zero;
        }

        public Vector2 ButtonToAxis(bool up, bool right, bool left, bool down)
        {
            int x = right ? 1 : 0;
            int y = down ? 1 : 0;
            x -= left ? 1 : 0;
            y -= up ? 1 : 0;

            return new(x, y);
        }

        public override readonly string ToString()
        {
            return Source switch
            {
                InputSource.Keyboard => _keyboard!.Value.ToString(),
                InputSource.Mouse => _mouse!.Value.ToString(),
                InputSource.Gamepad => _gamepad!.Value.ToString(),
                _ => "?",
            };
        }
    }
}
