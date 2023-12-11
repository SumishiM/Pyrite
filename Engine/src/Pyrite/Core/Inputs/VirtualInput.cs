namespace Pyrite.Core.Inputs
{
	public abstract class VirtualInput
	{
		protected const int MAX_INPUT_MEMORY = 2;

        public bool Consumed { get; protected set; } = false;
        public void Consume() => Consumed = true;
        public void Free() => Consumed = false;

        public Action<InputState>? OnPressed = null;
        public Action<InputState>? OnReleased = null;

        public abstract void Update(InputState state);
	}
}