using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Physics
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 Zero => new(0f, 0f);
        public static Vector2 UnitX => new(1f, 0f);
        public static Vector2 UnitY => new(0f, 1f);
        public static Vector2 One => new(1f, 1f);

        public float Length()
            => MathF.Sqrt(X * X + Y * Y);
        public float SqrLength() 
            => X * X + Y * Y;

        public void Normalize()
        {
            float length = Length();
            X *= 1f / length;
            Y *= 1f / length; 
        }

        public static float Dot(Vector2 u, Vector2 v) => u * v;
        public static float operator * (Vector2 u, Vector2 v) => u.X * v.X + u.Y * v.Y;

        public static Vector2 operator * (Vector2 v, float s) => new(v.X * s, v.Y * s);
        public static Vector2 operator * (Vector2 v, int s) => new(v.X * s, v.Y * s);

        public static Vector2 operator / (Vector2 v, float s) => new(v.X / s, v.Y / s);
        public static Vector2 operator / (Vector2 v, int s) => new(v.X / s, v.Y / s);

        public static Vector2 operator + (Vector2 u, Vector2 v) => new(u.X + v.X, u.Y + v.Y);
        public static Vector2 operator - (Vector2 u, Vector2 v) => new(u.X - v.X, u.Y - v.Y);
    }
}
