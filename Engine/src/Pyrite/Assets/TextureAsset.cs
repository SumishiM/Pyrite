using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Assets
{
    public class TextureAsset(string path) : GameAsset(path)
    {
		Texture Texture { get; set; } = Game.Instance.Content.Load<Texture2D>(path);
	}
}
