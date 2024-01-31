using System.Numerics;

namespace Pyrite.Core
{
    public class Transform
    {
        //A transform abstraction.
        //For a transform we need to have a position a scale and a rotation,
        //depending on what application you are creating, the type for these may vary.

        public Vector2 Position { get; set; } = new Vector2(0, 0);

        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Size;

        public float Rotation { get; set; } = 0f;

        public static Transform Empty => new();
    }
}
