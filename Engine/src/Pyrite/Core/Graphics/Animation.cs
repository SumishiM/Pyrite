using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using System.Collections.Immutable;

namespace Pyrite.Core.Graphics
{
    public struct AnimationComponent : IDrawable
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

        /// <summary>
        /// Animation <see cref="PlayMode"/>
        /// </summary>
        /// <value></value>
        public PlayMode Mode { get; set; }
        public Shader? Shader { get; set; }
        public Texture? Texture { get; init; }
        public int SortingOrder { get; set; }

        private float _maxDuration = -1f;

        public float MaxDuration
        {
            get
            {
                if (_maxDuration == -1f)
                {
                    _maxDuration = 0f;
                    foreach (var frame in _frames)
                    {
                        _maxDuration += frame.Duration;
                    }
                }
                return _maxDuration; 
            }
        }

        [Obsolete]
        public AnimationComponent() { }

        public AnimationComponent(string path, PlayMode playMode = PlayMode.Single, params FrameInfo[] frames)
        {
            Texture = new(path);
            Mode = playMode;
            _frames = [.. frames];
        }

        public AnimationComponent(Texture texture, PlayMode playMode = PlayMode.Single, params FrameInfo[] frames)
        {
            Texture = texture;
            Mode = playMode;
            _frames = [.. frames];
        }

        public void SetPlayMode(PlayMode playMode)
        {
            Mode = playMode;
        }

        public void Dispose()
        {
            Shader?.Dispose();
            Texture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
