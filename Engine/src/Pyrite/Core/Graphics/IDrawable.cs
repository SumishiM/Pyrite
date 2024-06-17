using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public interface IDrawable : IComponent, IDisposable
    {
        public Texture Texture { get; init; }
        public int SortingOrder { get; set; }

        
    }
}
