using Ignite;
using Ignite.Systems;
using Pyrite.Graphics;

namespace Pyrite.Core
{
    public class Scene
    {
        public string Name { get; set; } = "Unnamed Scene";

        private readonly World.Builder DefaultBuilder = World
            .CreateBuilder()
            .AddNode("Main Camera", new CameraComponent());


        private World? _world = null;
        public World World
        {
            get
            {
                if (_world == null)
                {
                    DefaultBuilder.AddSystems([.. _systems]);
                    _world = DefaultBuilder.Build();
                }
                return _world;
            }
        }

        private readonly List<Type> _systems = [];

        public Scene(params Type[] systems)
        {
            _systems = [.. _systems, .. systems];
        }

        internal void Start()
        {
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
