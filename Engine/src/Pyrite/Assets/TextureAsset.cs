using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Assets
{
    public class TextureAsset(string path) : GameAsset(path)
    {
        /// <summary>
        /// Loaded texture 
        /// </summary>
		public Texture2D Texture { get; internal set; } = Texture2D.FromStream(Game.GraphicsDevice, File.Open(path, FileMode.Open));
	}
}
