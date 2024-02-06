using Ignite;
using Pyrite.Components.Graphics;
using Pyrite.Core.Graphics;

namespace Pyrite.Core
{
    public class Scene
    {
        public string Name { get; set; } = "Unnamed Scene";

        private readonly World.Builder _defaultWorldBuilder = World
            .CreateBuilder()
            .AddNode("Main Camera", typeof(CameraComponent))
            .AddNode("Global Light");

        private World? _world = null;
        public World World
        {
            get
            {
                if (_world == null)
                {
                    _defaultWorldBuilder.AddSystems([.. _systems]);
                    _world = _defaultWorldBuilder.Build();
                }
                return _world;
            }
        }

        private readonly List<Type> _systems = [];

        public Scene(string name = "Unnamed Scene", params Type[] systems)
        {
            Name = name;
            _systems = [.. _systems, .. systems];
        }

        internal void Start()
        {
            Camera.Main.Zoom = 1f;
            World.Start();
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
    }
}
