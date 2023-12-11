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

		public bool Pressed => Down && (IntValue != IntPreviousValue);
		public bool PressedX => Down && (IntValue.X != IntPreviousValue.X);
		public bool PressedY => Down && (IntValue.Y != IntPreviousValue.Y);
		public bool Released => Previous && !Down;

		public bool Previous = false;
		public bool Down = false;

		public bool TickX;
		public bool TickY;

        public override void Update(InputState state)
        {
			Previous = Down;
			Down = false;
			PreviousValue = Value;
			IntPreviousValue = IntValue;
			Value = Vector2.Zero;

			foreach (var axis in _axis)
			{
				var v = axis.Check(state);

			}

			var lengthSq = Value.LengthSquared();
			if( lengthSq > 1)
				Value.Normalize();

            throw new NotImplementedException();
        }
    }
}