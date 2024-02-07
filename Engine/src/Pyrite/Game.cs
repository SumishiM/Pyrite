using Ignite;
using Pyrite.Core;
using Pyrite.Core.Inputs;
using Pyrite.Utils;
using System.Drawing;

namespace Pyrite
{
    public class Game : IDisposable
    {
        private static Window? _window = null;
        /// <summary>
        /// Main game window instance
        /// </summary>
        public static Window Window
        {
            get
            {
                if (_window == null)
                    throw new NullReferenceException("The game window isn't created yet.");
                return _window;
            }
        }

        private static Game? _instance = null;
        /// <summary>
        /// Game instance
        /// </summary>
        public static Game Instance
        {
            get
            {
                _instance ??= new Game();
                return _instance;
            }
        }

        private readonly World.Builder _defaultWorldBuilder = World
            .CreateBuilder();

        private World? _percistentWorld = null;

        /// <summary>
        /// <see cref="Ignite.World"/> that is running for the entire life cycle of the game.
        /// It will be destroyed when the game stops
        /// </summary>
        public World PercistentWorld
        {
            get
            {
                if (_percistentWorld == null)
                {
                    _defaultWorldBuilder.AddSystems([.. Systems]);
                    _percistentWorld = _defaultWorldBuilder.Build();
                }
                return _percistentWorld;
            }
        }

        protected virtual List<Type> Systems => [];

        public InputContextMapping Inputs;

        protected virtual WindowInfo WindowInfo => new()
        {
            Title = "Pyrite",
            BackgroundColor = Color.Black,

#if PS4 || XBOXONE
            Width = 1920,
            Height = 1080,
            Maximized = true,
            Resizable = false,
#elif NSWITCH
            Width = 1280,
            Height = 720,
            Maximized = true,
            Resizable = false,
#else
            Width = 1080,
            Height = 720,
            Maximized = false,
            Resizable = true,
#endif
        };

        public Game()
        {
#if DEBUG
            Console.WriteLine($"Initializing {WindowInfo.Title}");
#endif
            _instance = this;

            _window = new Window(WindowInfo);

            _window.OnLoad += OnLoad;
            _window.OnUpdate += OnUpdate;
            _window.OnRender += OnRender;
            _window.OnClose += OnClose;
        }

        /// <summary>
        /// Execute the game
        /// </summary>
        /// <exception cref="NullReferenceException"/>
        public void Run()
        {
#if DEBUG
            Console.WriteLine("Run Game");
#endif
            if (_window == null)
                throw new NullReferenceException();

            _window.Run();
        }

        private void OnLoad()
        {
#if DEBUG
            Console.WriteLine("Start Initialization Game");
#endif
            Initialize();
            PercistentWorld.Start();
            SceneManager.CurrentScene?.Start();
#if DEBUG
            Console.WriteLine("Finished Initialization Game");
#endif
        }

        private void OnUpdate(double deltaTime)
        {
            Time.Update(deltaTime);

            PercistentWorld.Update();
            PercistentWorld.FixedUpdate();

            SceneManager.CurrentScene?.Update();
            SceneManager.CurrentScene?.FixedUpdate();
        }

        private void OnRender()
        {
            _percistentWorld?.Render();
            SceneManager.CurrentScene?.Render();
        }

        private void OnClose()
        {
#if DEBUG
            Console.WriteLine("Close Game");
#endif
            PercistentWorld.Exit();
            SceneManager.CurrentScene?.Exit();
        }


        /// <summary>
        /// Called once on game initialisation. 
        /// Used to initialize game data.
        /// </summary>
        protected virtual void Initialize() { }


        public void Dispose()
        {
            PercistentWorld.Dispose();
            _instance = null;

            GC.SuppressFinalize(this);
        }
    }
}