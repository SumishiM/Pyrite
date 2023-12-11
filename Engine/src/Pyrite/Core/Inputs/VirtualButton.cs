namespace Pyrite.Core.Inputs
{
    public class VirtualButton : VirtualInput
    {
		public List<ButtonBinding> Buttons = [];
		public ButtonBinding?[] _lastPressed = new ButtonBinding?[MAX_INPUT_MEMORY];

		public bool Pressed => Down && !Previous && !Consumed;
		public bool Released => Previous && !Down;
		public bool Previous = false;
		public bool Down = false;

		public float LastPressed = 0f;
		public float LastReleased = 0f;



        public override void Update(InputState state)
        {
			Previous = Down;
			Down = false;

			foreach (var button in Buttons)
			{
				if (button.Check(state))
				{
					Down = true;
					// here set last inputs buffer
					break;
				}
			}

			if( !Down)
            {
                Consumed = false;
            }

			if (Pressed)
			{
				OnPressed?.Invoke(state);
				// definitly need to change
				LastPressed = DateTime.Now.Ticks;
			}

			if(Released)
			{
				OnReleased?.Invoke(state);
				LastReleased= DateTime.Now.Ticks;
			}

            throw new NotImplementedException();
        }
    }
}