namespace Pyrite.Core.Inputs
{
    public class VirtualButton : VirtualInput
    {
		public List<ButtonBinding> Buttons = [];
		public ButtonBinding?[] _lastPressed = new ButtonBinding?[MAX_INPUT_MEMORY];

		public bool Pressed;
		public bool Released;
		public bool Previous;
		public bool Down;

		public float LastPressed = 0f;
		public float LastReleased = 0f;


		public event Action<InputState>? OnPressed;
		public event Action<InputState>? OnReleased;


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
				Consumed = false;

		
		    throw new NotImplementedException();
        }
    }
}