using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public class VirtualAxis : IVirtualInput
    {
        public Vector2 PreviousValue { get; private set; }
        public Point IntPreviousValue { get; private set; }
        public Vector2 Value { get; private set; }
        public Point IntValue { get; private set; }
        public Point PressedValue { get; private set; }

        public void Update(ICollection<IInputDevice> devices)
        {
        }
    }
}
