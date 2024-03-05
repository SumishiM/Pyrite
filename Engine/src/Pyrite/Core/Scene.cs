using Ignite;
using Pyrite.Components.Graphics;

namespace Pyrite.Core
{
    public struct Scene : IDisposable
    {
        public string Name { get; set; } = "Unnamed Scene";

        private readonly World.Builder _defaultWorldBuilder = 
            World.CreateBuilder()
                .AddNode("Main Camera", typeof(CameraComponent))
                .AddNode("Global Light");

        private World? _world = null;

        /// <summary>
        /// Simulation world of the Scene. See Ignite.
        /// </summary>
        public World World
        {
            get
            {
                if (_world == null)
                {
                    // Build a world if it's the first time
                    _defaultWorldBuilder.AddSystems([.. _systems]);
                    _world = _defaultWorldBuilder.Build();
                }
                return _world;
            }
        }

        private readonly List<Type> _systems = [];
        private bool _isStarted = false;

        public Scene(string name = "Unnamed Scene", params Type[] systems)
        {
            Name = name;
            _systems = [.. _systems, .. systems];
        }

        internal void Start()
        {
            World.Start();
            _isStarted = true;
        }

        internal void Update()
        {
            World.Update();
        }

        internal void FixedUpdate()
        {
            World.FixedUpdate();
        }

        internal void Render()
        {
            World.Render();
        }

        internal void Exit()
        {
            if (_isStarted)
                World.Exit();
        }

        internal void Pause()
        {
            World.Pause();
        }

        internal void Resume()
        {
            World.Resume();
        }

        public void Dispose()
        {
            World.Dispose();
            _systems.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
