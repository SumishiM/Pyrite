using System.Collections.Immutable;

namespace Pyrite.Core.Inputs
{
    public class VirtualAxis : VirtualInput
    {
		public Vector2 PreviousValue;
		public Vector2 Value;
		public Point IntPreviousValue;
		public Point IntValue;

		public Point PressedValue;

		public ImmutableArray<AxisBinding> Axis => _axis;
		private ImmutableArray<AxisBinding> _axis = ImmutableArray.Create<AxisBinding>();

        public override void Update(InputState state)
        {
            throw new NotImplementedException();
        }
    }
}