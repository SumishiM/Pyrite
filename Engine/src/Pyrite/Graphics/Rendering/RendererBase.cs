namespace Pyrite.Graphics.Rendering
{
    public abstract class RendererBase : IDisposable
    {

        protected readonly List<Sprite> _sprites = [];

        public abstract unsafe void Initialize();
        public abstract void ClearScreen ();
        public abstract unsafe void Draw(Sprite sprite);
        public abstract void Dispose();
    }
}
