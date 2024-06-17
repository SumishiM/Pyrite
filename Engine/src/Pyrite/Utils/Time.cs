namespace Pyrite.Utils
{
    public static class Time
    {
        /// <summary>
        /// Scaled delta time between two game frames
        /// </summary>
        public static float DeltaTime { get; private set; }
        
        /// <summary>
        /// Raw delta time between two game frames
        /// </summary>
        public static float RawDeltaTime { get; private set; }
        
        /// <summary>
        /// Fixed delta time
        /// </summary>
        public static float FixedDeltaTime { get; internal set; }
        
        /// <summary>
        /// Time scaling for scaled delta time and scaled FPS
        /// </summary>
        public static float TimeScale { get; set; } = 1f;
        
        /// <summary>
        /// Scaled FPS value
        /// </summary>
        public static int FPS => (int)(1f / DeltaTime);
        
        /// <summary>
        /// Raw FPS value
        /// </summary>
        public static int RawFPS => (int)(1f / RawDeltaTime);
        
        /// <summary>
        /// Total scaled time elapsed 
        /// </summary>
        public static float TotalTime { get; private set; } = 0f;

        /// <summary>
        /// Total raw time elapsed 
        /// </summary>
        public static float RawTotalTime { get; private set; } = 0f;

        /// <summary>
        /// Update time values from an elapsed time
        /// </summary>
        internal static void Update(double deltaTime)
        {
            TotalTime += (float)deltaTime * TimeScale;

            RawTotalTime += (float)deltaTime;
            RawDeltaTime = (float)deltaTime;

            if (TimeScale == 0f)
                DeltaTime = 0f;
            else
                DeltaTime = RawDeltaTime * TimeScale;
        }
    }
}
