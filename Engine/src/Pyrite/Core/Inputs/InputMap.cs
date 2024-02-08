using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public struct InputMap
    {
        public List<InputAction> Actions;

        public List<IVirtualInput> VirtualInputs { get; set; }

        public void Update(ICollection<IInputDevice> devices)
        {
            foreach (var input in VirtualInputs)
            {
                input.Update(devices);
            }
        }
    }
}
