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
    }
}
