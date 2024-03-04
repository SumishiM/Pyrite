using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public struct InputMap
    {
        public List<InputAction> Actions;

        public Dictionary<string, List<IVirtualInput>> VirtualInputs { get; set; }
        public void Update(ICollection<IInputDevice> devices)
        {
            foreach (var device in devices)
            {
                foreach (var (name, inputs) in VirtualInputs)
                {
                    foreach(var input in inputs)
                    {
                        input.Update(device);
                    }
                }
            }
        }
    }
}
