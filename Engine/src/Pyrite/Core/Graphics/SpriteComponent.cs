using Ignite.Attributes;
using Pyrite.Components;
using Pyrite.Core.Graphics.Shaders;
using Pyrite.Utils;
using System.Numerics;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public class SpriteComponent : Component
    {
        public readonly Shader? Shader = null;
        public readonly Texture? Texture;

        public int SortingOrder { get; set; }

        public Matrix4x4 ModelMatrix
        {
            get
            {
                if (Texture == null)
                    return Matrix4x4.Identity;

                Transform transform = Parent.GetComponent<TransformComponent>();
                return
                    Matrix4x4.CreateScale(Texture.Size.X * transform.Scale.X, Texture.Size.Y * transform.Scale.Y, 1f) *
                    Matrix4x4.CreateRotationZ(transform.Rotation.ToRadians()) *
                    Matrix4x4.CreateTranslation(transform.Position.X, transform.Position.Y, 0f);
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
