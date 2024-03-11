using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public interface IDrawable : IComponent, IDisposable
    {
        public Shader? Shader { get; set; }
        public Texture? Texture { get; init; }
        public int SortingOrder { get; set; }

        public Matrix GetModelMatrix ( Transform transform )
        {
            if ( Texture == null )
                return Matrix.Identity;

            return
                Matrix.CreateScale(Texture.Size.X * transform.Scale.X, Texture.Size.Y * transform.Scale.Y, 1f) *
                Matrix.CreateRotationZ(transform.Rotation.ToRadians()) *
                Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0f);
        }
    }
}
