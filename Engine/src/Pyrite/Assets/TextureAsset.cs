using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Assets
{
    public class TextureAsset : GameAsset
    {

        protected Texture2D? _texture;
        /// <summary>
        /// Loaded texture 
        /// </summary>
		public virtual Texture2D Texture
        {
            get => _texture ?? Game.Data.MissingTexture;
            internal set => _texture = value;
        }

        public TextureAsset() { }

        public TextureAsset(string path) : base(path)
        {
            _texture = Texture2D.FromStream(Game.GraphicsDevice, File.Open(path, FileMode.Open));
        }
    }
}
