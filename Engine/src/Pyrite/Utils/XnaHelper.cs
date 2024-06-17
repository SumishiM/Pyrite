using Microsoft.Xna.Framework.Graphics;
using Pyrite.Core.Geometry;

namespace Pyrite.Utils
{
    public static class XnaHelper
    {
        public static Point Size(this Texture2D texture)
            => new(texture.Width, texture.Height);
    }

}