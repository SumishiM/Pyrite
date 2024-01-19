﻿namespace Pyrite.Utils
{
    public static class Time
    {
        public static float DeltaTime { get; private set; }
        public static float RawDeltaTime { get; private set; }
        public static float FixedDeltaTime => 0.2f;
        public static float TimeScale { get; set; } = 1f;

        public static void Update(GameTime gameTime)
        {
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeScale;
        }
    }
}
