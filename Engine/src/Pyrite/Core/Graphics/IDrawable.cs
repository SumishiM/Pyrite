using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;
using Pyrite.Core.Graphics.Rendering.OpenGL;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public interface IDrawable : IComponent, IDisposable
    {
        public Shader? Shader { get; set; }
        public Texture Texture { get; init; }
        public int SortingOrder { get; set; }

        
    }
}
