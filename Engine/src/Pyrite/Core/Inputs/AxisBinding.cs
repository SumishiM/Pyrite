using Microsoft.Xna.Framework.Input;

namespace Pyrite.Core.Inputs
{
	public readonly struct AxisBinding
	{
		public readonly ButtonBinding? Single;
		public readonly ButtonBinding Up;
		public readonly ButtonBinding Down;
		public readonly ButtonBinding Left;
		public readonly ButtonBinding Right;
		public readonly InputSource Source;

		public AxisBinding(ButtonBinding up, ButtonBinding down, ButtonBinding left, ButtonBinding right)
		{
			Source = up.Source;
			Up = up;
			Down = down;
			Left = left;
			Right = right;
		}

		public AxisBinding(Keys up, Keys down, Keys left, Keys right)
		{
			Source = InputSource.Keyboard;
			Up = new(up);
			Down = new(down);
			Left = new(left);
			Right = new(right);
		}

		public AxisBinding(Buttons up, Buttons down, Buttons left, Buttons right)
		{
			Source = InputSource.Keyboard;
			Up = new(up);
			Down = new(down);
			Left = new(left);
			Right = new(right);
		}

		public AxisBinding(GamepadAxis axis)
		{
			Source = InputSource.GamePadAxis;
			Single = new(axis);
		}

		public bool Check(InputState state)
		{
			return IsNotDefault(GetAxis(state));
		}

		public Vector2 GetAxis(InputState state)
        {
            if (Single != null)
                return Single.Value.GetAxis(state.Gamepad);

            int x = Right.Check(state) ? 1 : 0;
            int y = Down.Check(state) ? 1 : 0;
            x -= Left.Check(state) ? 1 : 0;
            y -= Up.Check(state) ? 1 : 0;

            return new(x, y);
        }

		private bool IsNotDefault(Vector2 axis)
		{
			return axis.X != 0f && axis.Y != 0f;	
		}
	}
}