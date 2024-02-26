using Ignite.Attributes;
using Ignite.Components;
using Ignite.Extentions;
using Pyrite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IComponent
    {
        public readonly Shader? Shader = null;
        public readonly Texture? Texture;

        public int SortingOrder { get; set; }

        public readonly Matrix GetModelMatrix(Transform transform)
        {
            if (Texture == null)
                return Matrix.Identity;

            return
                Matrix.CreateScale(Texture.Size.X * transform.Scale.X, Texture.Size.Y * transform.Scale.Y, 1f) *
                Matrix.CreateRotationZ(transform.Rotation.ToRadians()) *
                Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0f);
        }

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
