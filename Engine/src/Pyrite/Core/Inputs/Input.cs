using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public class Input
    {
        public static Point ScreenMousePosition { get; internal set; } = new Point(0, 0);
        public static Point WorldMousePosition { get; internal set; } = new Point(0, 0);

        public static Dictionary<Guid, IInputDevice> Devices { get; internal set; } = [];
    }

    
}
