using Ignite.Components;

namespace Pyrite.Core.Graphics
{
    /// <summary>
    /// All <see cref="Ignite.Systems.IRenderSystem"/>s filter out this component
    /// </summary>
    public readonly struct InvisibleComponent : IComponent
    {
        public InvisibleComponent() {}
    }
}