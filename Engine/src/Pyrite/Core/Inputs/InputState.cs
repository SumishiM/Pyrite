using Microsoft.Xna.Framework.Input;

namespace Pyrite.Core.Inputs
{
    public readonly struct InputState
    {
        public readonly KeyboardState Keyboard;
        public readonly MouseState Mouse;
        public readonly GamePadState Gamepad;

        public InputState (KeyboardState keyboard, MouseState mouse, GamePadState gamePad)
        {
            Keyboard = keyboard;
            Mouse = mouse;
            Gamepad = gamePad;
        }
    }
}
