using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Physics
{
    public class Rigidbody2D
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Force;
        public float Torque;
        public float Rotation;
        public float AngularVelocity;
        public float Mass;
        public float Inertia;
        public float Friction;
        public float Restitution;
        public Shape2D Shape;
    }
}
