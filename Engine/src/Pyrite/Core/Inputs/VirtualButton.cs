using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public class VirtualButton : IVirtualInput
    {
        public List<ButtonBinding> Bindings = [];

        // buffers
        public ButtonBinding[] LastKeyboardBindingPressedBuffer = new ButtonBinding[4];
        public ButtonBinding[] LastKeyboardBindingReleasedBuffer = new ButtonBinding[4];

        public ButtonBinding[] LastGamepadBindingPressedBuffer = new ButtonBinding[4];
        public ButtonBinding[] LastGamepadBindingReleasedBuffer = new ButtonBinding[4];

        public bool Pressed = false;
        public bool Released = false;
        public bool Previous = false;
        public bool Down = false;

        public bool Consumed = false;

        public float LastPressed = 0f;
        public float LastReleased = 0f;

        public event Action? OnPressed;
        public event Action? OnReleased;

        public void Update()
        {
            throw new NotImplementedException();
        }

        internal void Register(Key[] keys)
        {
            foreach (var key in keys)
            {
                Bindings.Add(new ButtonBinding(key));
            }
        }

        internal void Register(MouseButton[] mouseButtons)
        {
            foreach (var button in mouseButtons)
            {
                Bindings.Add(new ButtonBinding(button));
            }
        }

        internal void Register(GamepadButtons[] gamepadButtons)
        {
            foreach (var button in gamepadButtons)
            {
                Bindings.Add(new ButtonBinding(button));
            }
        }
    }
}
