using Pyrite.Utils;
using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public class VirtualButton : IVirtualInput
    {
        public List<ButtonBinding> Bindings = [];

        // buffers
        //public Queue<ButtonBinding> LastKeyboardBindingPressedBuffer = [];
        //public Queue<ButtonBinding> LastKeyboardBindingReleasedBuffer = [];

        //public Queue<ButtonBinding> LastGamepadBindingPressedBuffer = [];
        //public Queue<ButtonBinding> LastGamepadBindingReleasedBuffer = [];

        public bool Pressed = false;
        public bool Released = false;
        public bool Down = false;

        public bool Consumed = false;

        public float DownTime = 0f;
        public float LastPressed = 0f;
        public float LastReleased = 0f;

        public event Action<float>? OnDown;
        public event Action? OnPressed;
        public event Action? OnReleased;

        public VirtualButton(params ButtonBinding[] bindings)
        {
            Bindings.AddRange(bindings);
        }

        public void Update(IInputDevice device)
        {
            Consumed = false;
            Down = false;
            LastPressed += Time.DeltaTime;
            LastReleased += Time.DeltaTime;

            foreach (var binding in Bindings)
            {
                if (binding.IsPressed(device))
                {
                    Down = true;
                    break;
                }
            }

            if (Down) // check press
            {
                DownTime += Time.DeltaTime;
                OnDown?.Invoke(DownTime);
                if (!Pressed)
                {
                    LastPressed = 0f;
                    Pressed = true;
                    Released = false;
                    OnPressed?.Invoke();
                }
            }
            else // check release
            {
                DownTime = 0f;
                if (Pressed)
                {
                    LastReleased = 0f;
                    Pressed = false;
                    Released = true;
                    OnReleased?.Invoke();
                }
            }

        }

        public void Consume()
        {
            Consumed = true;
        }


        internal void Register(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                Bindings.Add(new(key));
            }
        }

        internal void Register(params MouseButtons[] mouseButtons)
        {
            foreach (var button in mouseButtons)
            {
                Bindings.Add(new(button));
            }
        }

        internal void Register(params GamepadButtons[] gamepadButtons)
        {
            foreach (var button in gamepadButtons)
            {
                Bindings.Add(new(button));
            }
        }

        internal void Unregister(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                Bindings.RemoveAll(b => b.ListenToInput(key));
            }
        }

        internal void Unregister(params MouseButtons[] mouseButtons)
        {
            foreach (var button in mouseButtons)
            {
                Bindings.RemoveAll(b => b.ListenToInput(button));
            }
        }

        internal void Unregister(params GamepadButtons[] gamepadButtons)
        {
            foreach (var button in gamepadButtons)
            {
                Bindings.RemoveAll(b => b.ListenToInput(button));
            }
        }
    }
}
