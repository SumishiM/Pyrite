using Microsoft.Xna.Framework.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct InputState(KeyboardState keyboard, MouseState mouse, GamePadState gamepad)
    {
        public readonly KeyboardState Keyboard = keyboard;
        public readonly MouseState Mouse = mouse;
        public readonly GamePadState Gamepad = gamepad;
    }


    public class InputContextMapping
    {
    }
}
