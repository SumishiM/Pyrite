using Microsoft.Xna.Framework.Graphics;
using Pyrite.Core.Geometry;

namespace Pyrite.Utils
{
    public static class XnaHelper
    {
        public static Point Size(this Texture2D texture)
            => new(texture.Width, texture.Height);

        public static Microsoft.Xna.Framework.Point ToPoint(this Microsoft.Xna.Framework.Vector2 v)
            => new(Calculator.RoundToInt(v.X), Calculator.RoundToInt(v.Y));
        public static Microsoft.Xna.Framework.Point ToPoint(this Microsoft.Xna.Framework.Vector3 v)
            => new(Calculator.RoundToInt(v.X), Calculator.RoundToInt(v.Y));
        public static Microsoft.Xna.Framework.Vector3 ToVector3(this Microsoft.Xna.Framework.Point v)
            => new(v.X, v.Y, 0f);
        public static Microsoft.Xna.Framework.Vector3 ToVector3(this Microsoft.Xna.Framework.Vector2 v)
            => new(v.X, v.Y, 0f);
        public static Microsoft.Xna.Framework.Vector3 ToXnaVector3(this Vector2 v)
            => new(v.X, v.Y, 0f);
    }

}