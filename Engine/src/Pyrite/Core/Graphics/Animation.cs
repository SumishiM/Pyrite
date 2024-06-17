using Pyrite.Core.Geometry;
using System.Collections.Immutable;

namespace Pyrite.Core.Graphics
{
    public struct Animation
    {
        /// <summary>
        /// Define the animation play mode
        /// </summary>
        public enum PlayMode
        {
            /// <summary>
            /// Play the animation once and end on the last frame
            /// </summary>
            Single = 0b00,

            /// <summary>
            /// Play the animation indefinitly
            /// </summary>
            Loop = 0b01,

            /// <summary>
            /// Play the animation once in reverse and end on the first frame
            /// </summary>
            SingleReverse = 0b10,


            /// <summary>
            /// Play the animation in reverse indefinitly
            /// </summary>
            LoopReverse = 0b11
        }

        /// <summary>
        /// Animation frame information
        /// </summary>
        public readonly struct FrameInfo
        {
            /// <summary>
            /// Size of the frame
            /// </summary>
            public readonly Point Size;

            /// <summary>
            /// Top left corner offset of the frame
            /// </summary>
            public readonly Point Offset;

            /// <summary>
            /// Duration of the frame
            /// </summary>
            public readonly float Duration;
        }

        /// <summary>
        /// Array of <see cref="FrameInfo"/>s of the animation 
        /// </summary>
        private readonly ImmutableArray<FrameInfo> _frames;

    }
}
