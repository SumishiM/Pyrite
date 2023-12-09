using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public enum InputSource
    {
        None = -1,
        Keyboard = 0,
        Mouse = 1,
        GamePad = 2,
        GamePadAxis = 3
    }

    public enum GamepadAxis
    {
        LeftThumb,
        RightThumb,
        Dpad
    }

    public enum MouseButtons
    {
        Left,
        Middle,
        Right
    }
}
