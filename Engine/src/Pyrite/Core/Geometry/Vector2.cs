using Pyrite.Utils;

namespace Pyrite.Core.Geometry
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public float X;
        public float Y;

        public Vector2(float x = 0f, float y = 0f)
        {
            X = x;
            Y = y;
        }

        #region Constants

        /// <summary>
        /// An empty vector [0, 0]
        /// </summary>
        public static Vector2 Zero => new(0f, 0f);

        /// <summary>
        /// A vector [1, 1]
        /// </summary>
        public static Vector2 One => new(1f, 1f);

        /// <summary>
        /// A vector [1, 0]
        /// </summary>
        public static Vector2 UnitX => new(1f, 0f);

        /// <summary>
        /// A vector [0, 1]
        /// </summary>
        public static Vector2 UnitY => new(0f, 1f);

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

        public static float Dot(Vector2 u, Vector2 v)
            => u.X * v.X + u.Y * v.Y;

        public static float Distance(Vector2 u, Vector2 v)
            => (u - v).Length();

        public static float SquaredDistance(Vector2 u, Vector2 v)
            => (u - v).SquaredLength();

        public static Vector2 Normalized(Vector2 u)
        {
            float length = u.Length();
            return new(u.X / length, u.Y / length);
        }
        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            return new Vector2(
                position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, 
                position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
        }

        public void Round()
        {
            X = Calculator.RoundToInt(X);
            Y = Calculator.RoundToInt(Y);
        }

        #region Operators

        public static implicit operator Point(Vector2 v) => new((int)v.X, (int)v.Y);
        
        public static implicit operator Vector2(System.Numerics.Vector2 v) => new(v.X, v.Y);
        public static implicit operator Vector2(Microsoft.Xna.Framework.Vector2 v) => new(v.X, v.Y);

        public static implicit operator System.Numerics.Vector2(Vector2 v) => new(v.X, v.Y);
        public static implicit operator Microsoft.Xna.Framework.Vector2(Vector2 v) => new(v.X, v.Y);

        public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2 a, Vector2 b) => a.X != b.X || a.Y != b.Y;

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator +(Vector2 a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator -(Vector2 a, Point b) => new(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector2 operator *(Vector2 a, Point b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator /(Vector2 a, Point b) => new(a.X / b.X, a.Y / b.Y);

        public static Vector2 operator *(int s, Vector2 p) => p * s;
        public static Vector2 operator *(Vector2 p, int s) => new(p.X * s, p.Y * s);

        public static Vector2 operator *(float s, Vector2 p) => p * s;
        public static Vector2 operator *(Vector2 p, float s) => new(p.X * s, p.Y * s);

        public static Vector2 operator /(Vector2 p, int s) => new(p.X / s, p.Y / s);
        public static Vector2 operator /(Vector2 p, float s) => new(p.X / s, p.Y / s);

        public static Vector2 operator -(Vector2 v) => new(-v.X, -v.Y);

        #endregion

        public override readonly string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public readonly bool AlmostEquals(Vector2 other, float epsilon = float.Epsilon)
        {
            return X + epsilon > other.X && X - epsilon < other.X 
                   && Y + epsilon > other.Y && Y - epsilon < other.Y; 
        }

        public readonly bool AlmostEquals(Vector2 A, Vector2 B, float epsilon = float.Epsilon)
        {
            return A.X + epsilon > B.X && A.X - epsilon < B.X 
                   && A.Y + epsilon > B.Y && A.Y - epsilon < B.Y; 
        }

        public readonly bool Equals(Vector2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Vector2 vector && Equals(vector);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
