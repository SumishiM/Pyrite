using Pyrite.Assets;
using Pyrite.Core.Geometry;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    public class Texture
    {
        internal Texture(string path)
        {
        }

        public static Texture Empty => Game.Data.MissingTexture;

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

        
    }
}
