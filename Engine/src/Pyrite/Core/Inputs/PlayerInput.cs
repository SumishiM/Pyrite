using Silk.NET.Input;
using Silk.NET.Vulkan;

namespace Pyrite.Core.Inputs
{
    public class PlayerInput
    {
        private IKeyboard? _keyboard;
        internal IKeyboard? Keyboard
        {
            get => _keyboard;
            set => _keyboard = value;
        }

        private IGamepad? _gamepad;
        internal IGamepad? Gamepad
        {
            get => _gamepad;
            set => _gamepad = value;
        }

        private IMouse? _mouse;
        internal IMouse? Mouse
        {
            get => _mouse;
            set => _mouse = value;
        }

        private List<IInputDevice> _devices = [];

        private InputMap map; // InputMap


        internal void UpdateDevices()
        {
            _devices = [];

            if (_keyboard is not null) _devices.Add(_keyboard);
            if (_gamepad is not null) _devices.Add(_gamepad);
            if (_mouse is not null) _devices.Add(_mouse);
        }

        public void Update()
        {
            map.Update(_devices);

        }

        void BindKeyUp(Keys key, Action callback)
        {
            
        }
    }
}
