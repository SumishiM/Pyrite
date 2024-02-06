using Ignite.Attributes;
using Pyrite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public class SpriteComponent : Component
    {
        public readonly Shader? Shader = null;
        public readonly Texture? Texture;

        public int SortingOrder { get; set; }

        public Matrix ModelMatrix
        {
            get
            {
                if (Texture == null)
                    return Matrix.Identity;

                Transform transform = Parent.GetComponent<TransformComponent>();
                return
                    Matrix.CreateScale(Texture.Size.X * transform.Scale.X, Texture.Size.Y * transform.Scale.Y, 1f) *
                    Matrix.CreateRotationZ(transform.Rotation.ToRadians()) *
                    Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0f);
            }
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
