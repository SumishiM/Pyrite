using Ignite;
using Pyrite.Core.Graphics.Rendering.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    public abstract class RendererBase : IDisposable
    {
        public abstract unsafe void Initialize();
        public abstract void ClearScreen ();
        public abstract unsafe void Draw(Transform transform, Texture texture, Shader? shader);
        public abstract void Dispose();
    }
}
