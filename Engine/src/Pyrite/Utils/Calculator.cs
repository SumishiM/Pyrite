using System.Numerics;

namespace Pyrite.Utils
{
	public static class Calculator
    {
        public static float ToRadians(this float degrees) => MathF.PI / 180f * degrees;
        public static float ToDegrees(this float radiants) => 180f / MathF.PI * radiants;

        public static int RoundToInt(this double v) 
			=> RoundToInt((float)v);
		public static int RoundToInt(this float v) 
			=> (int)MathF.Round(v);

        public static int FloorToInt(this double v)
            => FloorToInt((float)v);
        public static int FloorToInt(this float v)
            => (int)MathF.Floor(v);

        public static int CeilToInt(this double v)
            => CeilToInt((float)v);
        public static int CeilToInt(this float v)
            => (int)MathF.Ceiling(v);

        public static float Clamp01(this float v) 
			=> Math.Clamp(v, 0.0f, 1.0f);
		public static float Clamp01(this int v) 
			=> Math.Clamp(v, 0, 1);

		public static float Clamp(this float v, float min, float max) 
			=> Math.Clamp(v, min, max);
		public static float Clamp(this int v, int min, int max) 
			=> Math.Clamp(v, min, max);

		public static float Remap(this float value, float min1, float max1, float min2, float max2)
			=> min2 + (value - min1) * (max2 - min2) / (max1 - min1);

		public static float Remap(this float value, float min, float max)
			=> min + (value) * (max - min);

        public static float Min(params float[] values)
        {
            var min = float.MaxValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < min)
                    min = values[i];
            }

            return min;
        }

        public static float Max(params float[] values)
        {
            var max = float.MinValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > max)
                    max = values[i];
            }

            return max;
        }

        public static bool IsAlmostZero(this float value)
            => value < float.Epsilon && value > -float.Epsilon;
        public static bool IsAlmostZero(this Vector2 value)
            => value.X < float.Epsilon && value.X > -float.Epsilon
            && value.Y < float.Epsilon && value.Y > -float.Epsilon;

        public static float ClampNearZero(this float value, float minimum)
        {
            if (Math.Abs(value) < minimum)
                return minimum * Calculator.Sign(value);
                
            return value;
        }

        /// <returns>1 if positive or 0, -1 if negative</returns>
        private static float Sign(this float value)
        {
            if (value > 0)
                return 1;
            else if (value < 0)
                return -1;
            else
                return 1;
        }

        /// <returns>1 if positive or 0, -1 if negative</returns>
        private static int Sign(this int value)
        {
            if (value > 0)
                return 1;
            else if (value < 0)
                return -1;
            else
                return 1;
        }
        
        public static float Lerp(float origin, float target, float factor)
        {
            return origin * (1 - factor) + target * factor;
        }
        public static int LerpInt(float origin, float target, float factor)
        {
            return RoundToInt(origin * (1 - factor) + target * factor);
        }

        public static double LerpSnap(float origin, float target, double factor, float threshold = 0.01f)
        {
            return Math.Abs(target - origin) < threshold ? target : origin * (1 - factor) + target * factor;
        }

        public static float LerpSnap(float origin, float target, float factor, float threshold = 0.01f)
        {
            return Math.Abs(target - origin) < threshold ? target : origin * (1 - factor) + target * factor;
        }

        public static float LerpSmoothAngle(float a, float b, float deltaTime, float halLife)
        {
            a = NormalizeAngle(a);
            b = NormalizeAngle(b);
            float delta = MathF.Abs(a - b);
            if (delta > MathF.PI)
            {
                if (a > b)
                    a -= MathF.PI * 2;
                else
                    b -= MathF.PI * 2;
            }
            return LerpSmooth(a, b, deltaTime, halLife);
        }
        
        public static float LerpSmooth(float a, float b, float deltaTime, float halLife)
        {
            return Math.Abs(a- b) < 0.001f? b : b + (a - b) * float.Exp2(-deltaTime / halLife);
        }

        public static Vector2 LerpSmooth(Vector2 a, Vector2 b, float deltaTime, float halLife)
        {
            return new Vector2(LerpSmooth(a.X, b.X, deltaTime, halLife), LerpSmooth(a.Y, b.Y, deltaTime, halLife));
        }

        /// <summary>
        /// Normalizes the given angle to be within the range of 0 to 2π radians.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The normalized angle.</returns>
        public static float NormalizeAngle(float angle)
        {
            // Normalize the angle to be within the range [0, 2π)
            return (angle % (2 * MathF.PI) + 2 * MathF.PI) % (2 * MathF.PI);
        }
    }
}
