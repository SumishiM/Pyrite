using Pyrite.Utils;

namespace Pyrite.Core.Geometry
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x = 0f, float y = 0f, float z = 0f)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 v, float z = 0f)
        {
            X = v.X;
            Y = v.Y;
            Z = z;
        }

        #region Constants

        /// <summary>
        /// An empty vector [0, 0, 0]
        /// </summary>
        public static Vector3 Zero => new();

        /// <summary>
        /// A vector [1, 1, 1]
        /// </summary>
        public static Vector3 One => new(1f, 1f, 1f);

        /// <summary>
        /// A vector [1, 0, 0]
        /// </summary>
        public static Vector3 UnitX => new(1f, 0f, 0f);

        /// <summary>
        /// A vector [0, 1, 0]
        /// </summary>
        public static Vector3 UnitY => new(0f, 1f, 0f);

        /// <summary>
        /// A vector [0, 0, 1]
        /// </summary>
        public static Vector3 UnitZ => new(0f, 0f, 1f);

        #endregion

        public readonly float Length()
            => MathF.Sqrt(SquaredLength());

        public readonly float SquaredLength()
            => X * X + Y * Y;

        public void Normalize()
        {
            float length = Length();
            X /= length;
            Y /= length;
        }

        public static float Dot(Vector3 u, Vector3 v)
            => u.X * v.X + u.Y * v.Y + u.Z * v.Z;

        public static float Distance(Vector3 u, Vector3 v)
            => (u - v).Length();

        public static float SquaredDistance(Vector3 u, Vector3 v)
            => (u - v).SquaredLength();

        public static Vector3 Normalized(Vector3 u)
        {
            float length = u.Length();
            return new(u.X / length, u.Y / length);
        }

        public void Round()
        {
            X = Calculator.RoundToInt(X);
            Y = Calculator.RoundToInt(Y);
            Z = Calculator.RoundToInt(Z);
        }

        #region Operators
        public static implicit operator Point(Vector3 v) => new(Calculator.RoundToInt(v.X), Calculator.RoundToInt(v.Y));

        public static implicit operator Vector3(System.Numerics.Vector3 v) => new(v.X, v.Y, v.Z);
        public static implicit operator Vector3(Microsoft.Xna.Framework.Vector3 v) => new(v.X, v.Y, v.Z);

        public static implicit operator System.Numerics.Vector3(Vector3 v) => new(v.X, v.Y, v.Z);
        public static implicit operator Microsoft.Xna.Framework.Vector3(Vector3 v) => new(v.X, v.Y, v.Z);

        public static implicit operator Vector3(Vector2 v) => new(v.X, v.Y);
        public static implicit operator Vector2(Vector3 v) => new(v.X, v.Y);

        public static bool operator ==(Vector3 a, Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static bool operator !=(Vector3 a, Vector3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

        public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z + b.Z);

        public static Vector3 operator *(Vector3 a, Vector3 b) => new(a.X * b.X, a.Y * b.Y, a.Z / b.Z);
        public static Vector3 operator /(Vector3 a, Vector3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Vector3 operator *(int s, Vector3 p) => p * s;
        public static Vector3 operator *(Vector3 p, int s) => new(p.X * s, p.Y * s, p.Z * s);

        public static Vector3 operator *(float s, Vector3 p) => p * s;
        public static Vector3 operator *(Vector3 p, float s) => new(p.X * s, p.Y * s, p.Z * s);

        public static Vector3 operator /(Vector3 p, int s) => new(p.X / s, p.Y / s, p.Z / s);
        public static Vector3 operator /(Vector3 p, float s) => new(p.X / s, p.Y / s, p.Z / s);

        #endregion

        public override readonly string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }

        public readonly bool AlmostEquals(Vector3 other, float epsilon = float.Epsilon)
        {
            return X + epsilon > other.X && X - epsilon < other.X 
                   && Y + epsilon > other.Y && Y - epsilon < other.Y
                   && Z + epsilon > other.Z && Z - epsilon < other.Z; 
        }

        public readonly bool AlmostEquals(Vector3 A, Vector3 B, float epsilon = float.Epsilon)
        {
            return A.X + epsilon > B.X && A.X - epsilon < B.X 
                   && A.Y + epsilon > B.Y && A.Y - epsilon < B.Y
                   && A.Z + epsilon > B.Z && A.Z - epsilon < B.Z; 
        }

        public readonly bool Equals(Vector3 other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Vector3 vector && Equals(vector);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
	}
}