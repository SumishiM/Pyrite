using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Assets
{
    public class TextureAsset : GameAsset
    {
        /// <summary>
        /// Loaded texture 
        /// </summary>
		public Texture2D Texture { get; internal set; }

        public TextureAsset() {  }

        public TextureAsset(string path) : base(path)
        {
            Texture = Texture2D.FromStream(Game.GraphicsDevice, File.Open(path, FileMode.Open));
        }
    }
}
