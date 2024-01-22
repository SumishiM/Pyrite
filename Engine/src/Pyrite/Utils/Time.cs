﻿namespace Pyrite.Utils
{
    public static class Time
    {
        public static float DeltaTime { get; private set; }
        public static float RawDeltaTime { get; private set; }
        public static float FixedDeltaTime => 0.2f;
        public static float TimeScale { get; set; } = 1f;
        public static int FPS => (int)(1f / DeltaTime);
        public static int RawFPS => (int)(1f / RawDeltaTime);

        public static void Update(double deltaTime)
        {
            RawDeltaTime = (float)deltaTime;

            if (TimeScale == 0f)
                DeltaTime = 0f;
            else
                DeltaTime = RawDeltaTime / TimeScale;
        }
    }
}