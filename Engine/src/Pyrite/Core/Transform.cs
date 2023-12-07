using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pyrite.Core
{
    public class Transform : Component
    {
        public Point Position { get; set; } = Point.Zero;
        public float Rotation { get; set; } = 0f;
        public Vector2 Scale { get; set; } = Vector2.One;
        public int DepthLayer { get; set; } = 0;


        public static Transform Default => new();

        private Transform() { }
        public Transform(Point position, float rotation, Vector2 scale, int depthLayer = 0)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            DepthLayer = depthLayer;
        }
    }
}
