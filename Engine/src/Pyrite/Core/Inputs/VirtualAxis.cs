using Pyrite.Core.Geometry;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public class VirtualAxis : IVirtualInput
    {
        internal AxisBinding Binding = default;

        public Vector2 PreviousValue { get; private set; }
        public Point IntPreviousValue { get; private set; }
        public Vector2 Value { get; private set; }
        public Point IntValue { get; private set; }

        internal void Update(ICollection<IInputDevice> devices)
        {
            PreviousValue = Value;
            IntPreviousValue = IntValue;

            Value = Binding.GetAxis();
            IntValue = Value;
        }

        internal void Register(ButtonBinding up, ButtonBinding down, ButtonBinding left, ButtonBinding right)
        {
            Binding = new AxisBinding(up, down, left, right);
        }

        internal void Register(Key up, Key down, Key left, Key right)
        {
            Binding = new AxisBinding(up, down, left, right);
        }

        internal void Register(GamepadButtons up, GamepadButtons down, GamepadButtons left, GamepadButtons right)
        {
            Binding = new AxisBinding(up, down, left, right);
        }

        internal void Register(GamepadAxis axis)
        {
            Binding = new AxisBinding(axis);
        }
    }
}
