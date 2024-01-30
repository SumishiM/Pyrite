namespace Pyrite.Core.Graphics.Rendering
{
    public abstract class Renderer : IDisposable
    {

        protected readonly List<Sprite> _sprites = [];

        public abstract unsafe void Initialize();
        public void Queue ( Sprite sprite ) => _sprites.Add(sprite);
        public abstract unsafe void Draw();
        public abstract void Dispose();
    }
}
