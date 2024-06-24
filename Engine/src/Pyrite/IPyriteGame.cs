using Pyrite.Core.Geometry;

namespace Pyrite
{
    public interface IPyriteGame
    {
        public virtual WindowInfo GameWindowInfo => WindowInfo.Default;

        public string Name { get; }
        public Version Version => new(0, 0, 0, 0);


        /// <summary>
        /// List of systems to register in <see cref="PercistentWorld"/> on game start
        /// </summary>
        public List<Type> PercistentSystems => [];

        public virtual void Initialize() { }

        public virtual void LoadContent() { }
        public virtual Task LoadContentAsync() => Task.CompletedTask;

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnDraw() { }
        public virtual void OnExit() { }
    }
}