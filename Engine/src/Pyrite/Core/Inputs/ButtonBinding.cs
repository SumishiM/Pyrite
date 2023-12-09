using System.Diagnostics.Tracing;
using Microsoft.Xna.Framework.Input;

namespace Pyrite.Core.Inputs
{
	public readonly struct ButtonBinding
	{
		public readonly InputSource Source = InputSource.None;
		private readonly Keys? _keyboard = null;
		private readonly Buttons? _gamepad = null;
		private readonly MouseButtons? _mouse = null;
		private readonly GamepadAxis? _axis = null;

		public ButtonBinding(Keys key)
		{
			Source = InputSource.Keyboard;
			_keyboard = key;
		}

		public ButtonBinding(Buttons button)
		{
			Source = InputSource.GamePad;
			_gamepad = button;
		}

		public ButtonBinding(MouseButtons button)
		{
			Source = InputSource.Mouse;
			_mouse = button;
		}

		public ButtonBinding(GamepadAxis axis)
		{
			Source = InputSource.GamePadAxis;
			_axis = axis;
		}

		/// <returns>true if the input is down</returns>
		public readonly bool Check(InputState state)
		{
			return Source switch
			{
				InputSource.None => false,
				InputSource.Keyboard => state.Keyboard.IsKeyDown(_keyboard!.Value),
				InputSource.GamePad => state.Gamepad.IsButtonDown(_gamepad!.Value),
				InputSource.Mouse => _mouse!.Value switch
				{
					MouseButtons.Left => state.Mouse.LeftButton == ButtonState.Pressed,
					MouseButtons.Middle => state.Mouse.MiddleButton == ButtonState.Pressed,
					MouseButtons.Right => state.Mouse.RightButton == ButtonState.Pressed,
					_ => false,
				},
				_ => false,
			};
		}

		public readonly Vector2 GetAxis(GamePadState state)
		{
			if (Source != InputSource.GamePadAxis)
				return Vector2.Zero;

			return _axis!.Value switch
			{
				GamepadAxis.LeftThumb => new(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y),
				GamepadAxis.RightThumb => new(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y),
				GamepadAxis.Dpad => AxisFromButtons(
										state.DPad.Up == ButtonState.Pressed,
										state.DPad.Down == ButtonState.Pressed,
										state.DPad.Left == ButtonState.Pressed,
										state.DPad.Right == ButtonState.Pressed),

				_ => throw new Exception($"Gamepad axis '{_axis}' is not supported!")
			};
		}

		public static Vector2 AxisFromButtons(bool up, bool down, bool left, bool right)
		{
			int x = right ? 1 : 0;
			int y = down ? 1 : 0;
			x -= left ? 1 : 0;
			y -= up ? 1 : 0;

			return new(x, y);
		}
	}
}