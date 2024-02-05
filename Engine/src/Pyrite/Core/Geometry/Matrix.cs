using System.Numerics;

namespace Pyrite.Core.Geometry
{
    public readonly struct Matrix : IEquatable<Matrix>
    {
        public readonly float M11;
        public readonly float M12;
        public readonly float M13;
        public readonly float M14;

        public readonly float M21;
        public readonly float M22;
        public readonly float M23;
        public readonly float M24;

        public readonly float M31;
        public readonly float M32;
        public readonly float M33;
        public readonly float M34;

        public readonly float M41;
        public readonly float M42;
        public readonly float M43;
        public readonly float M44;

        public Matrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            (M11, M12, M13, M14) = (m11, m12, m13, m14);
            (M21, M22, M23, M24) = (m21, m22, m23, m24);
            (M31, M32, M33, M34) = (m31, m32, m33, m34);
            (M41, M42, M43, M44) = (m41, m42, m43, m44);
        }
        public static Matrix Identity => Matrix4x4.Identity;

        public readonly override bool Equals(object? obj)
        {
            return obj is Matrix matrix && Equals(matrix);
        }

        public readonly bool Equals(Matrix other)
        {
            return M11 == other.M11 &&
                M22 == other.M22 &&
                M33 == other.M33 &&
                M44 == other.M44 &&
                M12 == other.M12 &&
                M13 == other.M13 &&
                M14 == other.M14 &&
                M21 == other.M21 &&
                M23 == other.M23 &&
                M24 == other.M24 &&
                M31 == other.M31 &&
                M32 == other.M32 &&
                M34 == other.M34 &&
                M41 == other.M41 &&
                M42 == other.M42 &&
                M43 == other.M43;
        }

        public override readonly int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(M11, M12, M13, M14),
                HashCode.Combine(M21, M22, M23, M24),
                HashCode.Combine(M31, M32, M33, M34),
                HashCode.Combine(M41, M42, M43, M44));


        public readonly Matrix4x4 ToSysMatrix() =>
            new(M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44);

        public static implicit operator Matrix(Matrix4x4 m) =>
            new(m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);

        public static implicit operator Matrix4x4(Matrix m) =>
            new(m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);

        public static bool operator ==(Matrix left, Matrix right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }
    }
}
