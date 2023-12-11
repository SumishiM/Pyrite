using Microsoft.Xna.Framework.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct InputState(KeyboardState keyboard, MouseState mouse, GamePadState gamePad)
    {
        public readonly KeyboardState Keyboard = keyboard;
        public readonly MouseState Mouse = mouse;
        public readonly GamePadState Gamepad = gamePad;
    }
}
