using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Pyrite.Utils;
using System.Collections.Immutable;

namespace Pyrite.Core.Graphics
{
    public struct AnimationComponent : IDrawable
    {
        public struct FrameInfo
        {
            public Point Size;
            public Point Offset;
            public float Duration;
        }

        private readonly ImmutableArray<FrameInfo> _frames;

        public Shader? Shader { get; set; }
        public Texture? Texture { get; init; }
        public int SortingOrder { get; set; }

        public AnimationComponent () { }

        public AnimationComponent ( string path, params FrameInfo[] frames )
        {
            Texture = new(path);
            _frames = [.. frames];
        }

        public void Dispose ()
        {
            Shader?.Dispose();
            Texture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
