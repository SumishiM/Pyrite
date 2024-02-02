namespace Pyrite.Core.Graphics.Rendering
{
    public abstract class RendererBase : IDisposable
    {
        public abstract unsafe void Initialize();
        public abstract void ClearScreen ();
        public abstract unsafe void Draw(SpriteComponent sprite);
        public abstract void Dispose();
    }
}
