using Silk.NET.Core.Native;
using Silk.NET.Input;
using System.Numerics;

namespace Pyrite.Core.Geometry
{
    public struct Matrix : IEquatable<Matrix>
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;

        public float M21;
        public float M22;
        public float M23;
        public float M24;

        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public float M41;
        public float M42;
        public float M43;
        public float M44;

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


        public static Matrix CreateTranslation(float x, float y, float z)
        {
            CreateTranslation(x, y, z, out var result);
            return result;
        }

        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            CreateTranslation(position.X, position.Y, position.Z, out result);
        }

        public static void CreateTranslation(float x, float y, float z, out Matrix result)
        {
            result = new(
                1f, 0f, 0f, 0f,
                0f, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                x, y, z, 1f);
        }

        public static Matrix CreateRotationZ(float radians)
        {
            CreateRotationZ(radians, out var result);
            return result;
        }

        public static void CreateRotationZ(float radians, out Matrix result)
        {
            result = Identity;
            float num = MathF.Cos(radians);
            float num2 = MathF.Sin(radians);
            result.M11 = num;
            result.M12 = num2;
            result.M21 = 0f - num2;
            result.M22 = num;
        }

        public readonly Matrix4x4 ToSysMatrix() =>
            new(M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44);


        public static void CreateScale(float scale, out Matrix result)
        {
            CreateScale(scale, scale, scale, out result);
        }

        public static Matrix CreateScale(float xScale, float yScale, float zScale)
        {
            CreateScale(xScale, yScale, zScale, out var result);
            return result;
        }

        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
        {
            result.M11 = xScale;
            result.M12 = 0f;
            result.M13 = 0f;
            result.M14 = 0f;
            result.M21 = 0f;
            result.M22 = yScale;
            result.M23 = 0f;
            result.M24 = 0f;
            result.M31 = 0f;
            result.M32 = 0f;
            result.M33 = zScale;
            result.M34 = 0f;
            result.M41 = 0f;
            result.M42 = 0f;
            result.M43 = 0f;
            result.M44 = 1f;
        }

        public static Matrix Invert(Matrix matrix)
        {
            Invert(ref matrix, out var result);
            return result;
        }

        public static void Invert(ref Matrix matrix, out Matrix result)
        {
            float m = matrix.M11;
            float m2 = matrix.M12;
            float m3 = matrix.M13;
            float m4 = matrix.M14;
            float m5 = matrix.M21;
            float m6 = matrix.M22;
            float m7 = matrix.M23;
            float m8 = matrix.M24;
            float m9 = matrix.M31;
            float m10 = matrix.M32;
            float m11 = matrix.M33;
            float m12 = matrix.M34;
            float m13 = matrix.M41;
            float m14 = matrix.M42;
            float m15 = matrix.M43;
            float m16 = matrix.M44;
            float num = (float)((double)m11 * (double)m16 - (double)m12 * (double)m15);
            float num2 = (float)((double)m10 * (double)m16 - (double)m12 * (double)m14);
            float num3 = (float)((double)m10 * (double)m15 - (double)m11 * (double)m14);
            float num4 = (float)((double)m9 * (double)m16 - (double)m12 * (double)m13);
            float num5 = (float)((double)m9 * (double)m15 - (double)m11 * (double)m13);
            float num6 = (float)((double)m9 * (double)m14 - (double)m10 * (double)m13);
            float num7 = (float)((double)m6 * (double)num - (double)m7 * (double)num2 + (double)m8 * (double)num3);
            float num8 = (float)(0.0 - ((double)m5 * (double)num - (double)m7 * (double)num4 + (double)m8 * (double)num5));
            float num9 = (float)((double)m5 * (double)num2 - (double)m6 * (double)num4 + (double)m8 * (double)num6);
            float num10 = (float)(0.0 - ((double)m5 * (double)num3 - (double)m6 * (double)num5 + (double)m7 * (double)num6));
            float num11 = (float)(1.0 / ((double)m * (double)num7 + (double)m2 * (double)num8 + (double)m3 * (double)num9 + (double)m4 * (double)num10));
            result.M11 = num7 * num11;
            result.M21 = num8 * num11;
            result.M31 = num9 * num11;
            result.M41 = num10 * num11;
            result.M12 = (float)(0.0 - ((double)m2 * (double)num - (double)m3 * (double)num2 + (double)m4 * (double)num3)) * num11;
            result.M22 = (float)((double)m * (double)num - (double)m3 * (double)num4 + (double)m4 * (double)num5) * num11;
            result.M32 = (float)(0.0 - ((double)m * (double)num2 - (double)m2 * (double)num4 + (double)m4 * (double)num6)) * num11;
            result.M42 = (float)((double)m * (double)num3 - (double)m2 * (double)num5 + (double)m3 * (double)num6) * num11;
            float num12 = (float)((double)m7 * (double)m16 - (double)m8 * (double)m15);
            float num13 = (float)((double)m6 * (double)m16 - (double)m8 * (double)m14);
            float num14 = (float)((double)m6 * (double)m15 - (double)m7 * (double)m14);
            float num15 = (float)((double)m5 * (double)m16 - (double)m8 * (double)m13);
            float num16 = (float)((double)m5 * (double)m15 - (double)m7 * (double)m13);
            float num17 = (float)((double)m5 * (double)m14 - (double)m6 * (double)m13);
            result.M13 = (float)((double)m2 * (double)num12 - (double)m3 * (double)num13 + (double)m4 * (double)num14) * num11;
            result.M23 = (float)(0.0 - ((double)m * (double)num12 - (double)m3 * (double)num15 + (double)m4 * (double)num16)) * num11;
            result.M33 = (float)((double)m * (double)num13 - (double)m2 * (double)num15 + (double)m4 * (double)num17) * num11;
            result.M43 = (float)(0.0 - ((double)m * (double)num14 - (double)m2 * (double)num16 + (double)m3 * (double)num17)) * num11;
            float num18 = (float)((double)m7 * (double)m12 - (double)m8 * (double)m11);
            float num19 = (float)((double)m6 * (double)m12 - (double)m8 * (double)m10);
            float num20 = (float)((double)m6 * (double)m11 - (double)m7 * (double)m10);
            float num21 = (float)((double)m5 * (double)m12 - (double)m8 * (double)m9);
            float num22 = (float)((double)m5 * (double)m11 - (double)m7 * (double)m9);
            float num23 = (float)((double)m5 * (double)m10 - (double)m6 * (double)m9);
            result.M14 = (float)(0.0 - ((double)m2 * (double)num18 - (double)m3 * (double)num19 + (double)m4 * (double)num20)) * num11;
            result.M24 = (float)((double)m * (double)num18 - (double)m3 * (double)num21 + (double)m4 * (double)num22) * num11;
            result.M34 = (float)(0.0 - ((double)m * (double)num19 - (double)m2 * (double)num21 + (double)m4 * (double)num23)) * num11;
            result.M44 = (float)((double)m * (double)num20 - (double)m2 * (double)num22 + (double)m3 * (double)num23) * num11;
        }

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

        public static Matrix operator *(Matrix left, Matrix right)
        {
            return (Matrix4x4)left * (Matrix4x4)right;
        }
    }
}
