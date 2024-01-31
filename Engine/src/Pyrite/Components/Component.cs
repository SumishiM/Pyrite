using Ignite;
using Ignite.Components;

namespace Pyrite.Components
{
    public abstract class Component : IComponent
    {
        // set in ignite
#nullable disable
        public Node Parent { get; set; }
#nullable enable

        protected Component()
        {
        }
    }
}
