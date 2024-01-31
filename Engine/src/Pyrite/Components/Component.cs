using Ignite;
using Ignite.Components;

namespace Pyrite.Components
{
    public abstract class Component : IComponent
    {
        // set in ignite
        public Node Parent { get; set; }

        protected Component()
        {
        }

    }
}
