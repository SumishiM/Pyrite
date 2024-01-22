namespace Pyrite.Core.Graphics.Rendering
{
    public abstract class Renderer : IDisposable
    {

        public abstract unsafe void Initialize();
        public abstract unsafe void Draw();
        public abstract void Dispose();
    }
}
