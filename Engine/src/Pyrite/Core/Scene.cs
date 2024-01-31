using Ignite;
using Ignite.Systems;

namespace Pyrite.Core
{
    public class Scene
    {
        public string Name { get; set; } = "Unnamed Scene";

        private World? _world = null;
        public World World
        {
            get
            {
                if(_world == null)
                {
                    List<ISystem> systems = [];
                    foreach (Type type in _systems)
                    {
                        if (Activator.CreateInstance(type) is ISystem system)
                            systems.Add(system);
                        else
                            throw new Exception("Trying to create a system that isn't one.");
                    }
                    _world = new World(systems);
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
            //World.FixedUpdate();
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
