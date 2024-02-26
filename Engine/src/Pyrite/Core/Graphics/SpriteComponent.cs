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
        private Transform? _transform;
        internal Transform? Transform
        {
            get
            {
                _transform ??= this.GetFromRequirement<TransformComponent>();
                return _transform;
            }
            set => _transform = value;
        }

        public int SortingOrder { get; set; }

        public Matrix ModelMatrix
        {
            get
            {
                if (Texture == null || Transform == null)
                    return Matrix.Identity;
#nullable disable
                return
                    Matrix.CreateScale(Texture.Size.X * _transform.Scale.X, Texture.Size.Y * _transform.Scale.Y, 1f) *
                    Matrix.CreateRotationZ(_transform.Rotation.ToRadians()) *
                    Matrix.CreateTranslation(_transform.Position.X, _transform.Position.Y, 0f);
#nullable enable
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
