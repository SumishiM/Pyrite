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
                        //else
                        // throw warning 
                    }
                    _world = new World(Array.Empty<ISystem>());
                }
                return _world;
            }
        }

        private List<Type> _systems = [];

        internal Scene()
        {

        }

        internal void Start()
        {
            World.Start();
        }

        internal void Update()
        {
            World.Update();
        }

        internal void Render()
        {
            World.Render();
        }

        internal void Exit()
        {
            World.Exit();
        }
    }
}
