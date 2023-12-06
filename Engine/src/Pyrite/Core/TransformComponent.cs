using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core
{
    public class TransformComponent : Component
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
        public Vector3 Scale { get; set; } = Vector3.One;

        public TransformComponent()
        {

        }
        public TransformComponent(Vector3 position)
        {
            Position = position;
        }

        public TransformComponent(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public TransformComponent(Vector3 position, Quaternion rotation)
            : this(position)
        {
            Rotation = rotation;
        }

        public TransformComponent(Vector3 position, Quaternion rotation, Vector3 scale) 
            : this(position, rotation)
        {
            Scale = scale;
        }
    }

    public sealed class Transform
    {
        public static TransformComponent Forward => new();
        public static TransformComponent Back => new();
    }
}
