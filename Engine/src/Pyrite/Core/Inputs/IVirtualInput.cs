using Silk.NET.Input;

namespace Pyrite.Core.Inputs
{
    public interface IVirtualInput
    {
        internal void Update(IInputDevice device);
    }
}
