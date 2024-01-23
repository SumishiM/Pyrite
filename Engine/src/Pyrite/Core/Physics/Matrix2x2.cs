namespace Pyrite.Core.Physics
{
    public class Matrix2x2
    {
        float m00, m01;
        float m10, m11;

        public Matrix2x2(float m00, float m01, float m10, float m11) {
            this.m00 = m00; 
            this.m01 = m01;
            this.m10 = m10; 
            this.m11 = m11;
        }

        /// <summary>
        /// Create a matrix from angle in radiant
        /// </summary>
        /// <param name="radians"></param>
        public static Matrix2x2 FromAngle(float radians)
        {
            float c = MathF.Cos(radians);
            float s = MathF.Sin(radians);

            return new Matrix2x2(
                c, -s,
                s, c);
        }

        public Matrix2x2 Transpose()
        {
            return new Matrix2x2(
                m00, m10,
                m01, m11);
        }

        public static Vector2 operator *(Matrix2x2 m, Vector2 v)
        {
            return new Vector2(m.m00 * v.X + m.m01 * v.Y, m.m10 * v.X + m.m11 * v.Y);
        }

        public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(
              a.m00 * b.m00 + a.m01 * b.m10, a.m00 * b.m01 + a.m01 * b.m11,
              a.m10 * b.m00 + a.m11 * b.m10, a.m10 * b.m01 + a.m11 * b.m11);
        }
    }
}
