using Pyrite.Core.Geometry;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics.Rendering
{
    public struct BatchItem
    {
        public Texture Texture { get; set; }
        public Transform Transform { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2[] TexCoords { get; set; }
        public Matrix TransformMatrix
            => Matrix.CreateScale(Transform.Scale.X * Texture.Size.X, Transform.Scale.Y * Texture.Size.Y, 0f)
            * Matrix.CreateRotationZ(Transform.Rotation.ToRadians())
            * Matrix.CreateTranslation(Transform.Position.X, Transform.Position.Y, 0f);
    }
}
