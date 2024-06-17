using Pyrite.Core.Geometry;
using Pyrite.Utils;

namespace Pyrite.Core.Inputs
{
    public class VirtualAxis : IVirtualInput
    {
        internal AxisBinding Binding = default;

        public Vector2 PreviousValue { get; private set; }
        public Point IntPreviousValue { get; private set; }
        public Vector2 Value { get; private set; }
        public Point IntValue { get; private set; }

        public event Action<Vector2>? OnValueChanged;

        public bool IsAlmostZero => Calculator.IsAlmostZero(Value);

        public void Update(InputState state)
        {
            PreviousValue = Value;
            IntPreviousValue = IntValue;

            Value = Binding.GetAxis(state);
            IntValue = Value;

            if (Value != PreviousValue)
                OnValueChanged?.Invoke(Value);
        }

        internal void Register(ButtonBinding up, ButtonBinding down, ButtonBinding left, ButtonBinding right)
        {
            Binding = new(up, down, left, right);
        }

        internal void Register(Keys up, Keys down, Keys left, Keys right)
        {
            Binding = new(up, down, left, right);
        }

        internal void Register(GamepadButtons up, GamepadButtons down, GamepadButtons left, GamepadButtons right)
        {
            Binding = new(up, down, left, right);
        }

        internal void Register(GamepadAxis axis)
        {
            Binding = new(axis);
        }

        internal void Register(AxisBinding axis)
        {
            Binding = axis;
        }
    }
}
