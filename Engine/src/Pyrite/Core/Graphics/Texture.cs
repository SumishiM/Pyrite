using Pyrite.Assets;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    public class Texture : OGLTexture
    {
        internal Texture(string path) : base(path)
        {
        }

        public static Texture Create(string path)
        {
            // try get asset from database
            if (Game.Data.TryGetTexture(path, out Texture? texture))
                return texture;

            // Create and add texture to database
            texture = new Texture(path);

            Game.Data.AddTexture(path, texture);
            return texture;
        }

        internal Matrix GetModelMatrix(Transform transform)
        {
            return
                Matrix.CreateScale(Size.X * transform.Scale.X, Size.Y * transform.Scale.Y, 1f) *
                Matrix.CreateRotationZ(transform.Rotation.ToRadians()) *
                Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0f);
        }
    }
}
