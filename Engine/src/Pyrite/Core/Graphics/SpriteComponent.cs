using Ignite.Attributes;
using Ignite.Components;
using Ignite.Extentions;
using Pyrite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;
using System.Runtime.CompilerServices;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IDrawable
    {
        public Shader? Shader { get; set; } = null;
        public Texture? Texture { get; init; } = null;

        public int SortingOrder { get; set; }

        public SpriteComponent() { }
        public SpriteComponent(string path)
        {
            Texture = new(path);
        }

        public void Dispose()
        {
            Shader?.Dispose();
            Texture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
