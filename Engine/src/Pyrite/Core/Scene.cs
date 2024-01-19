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
                _world ??= new World(Array.Empty<ISystem>());
                return _world;
            }
        }

        private List<Type> _systems = [];

        public Scene()
        {
            List<ISystem> systems = [];
            foreach (Type type in _systems)
            {
                if (Activator.CreateInstance(type) is ISystem system)
                    systems.Add(system);
                //else
                // throw warning 
            }

            _world ??= new World(systems);
        }

        public void Start()
        {
            World.Start();
        }

        public void Update()
        {
            World.Update();
        }

        public void Render()
        {
            World.Render();
        }

        public void Exit()
        {
            World.Exit();
        }
    }
}
