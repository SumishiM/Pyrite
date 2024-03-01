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
        /// <see cref="Ignite.World"/> which run for the entire life cycle of the game.
        /// It will be destroyed when the game stops
        /// </summary>
        public World PercistentWorld
        {
            get
            {
                if (_percistentWorld == null)
                {
                    _defaultWorldBuilder.AddSystems([.. PercistantSystems]);
                    _percistentWorld = _defaultWorldBuilder.Build();
#if DEBUG
                    Console.WriteLine($"Percistent world built with {PercistantSystems.Count} systems.");
#endif
                }
                return _percistentWorld;
            }
        }

        /// <summary>
        /// List of systems to register in <see cref="PercistentWorld"/> on game start
        /// </summary>
        protected virtual List<Type> PercistantSystems => [];

        /// <summary>
        /// Input system instance
        /// </summary>
        public InputContextMapping Inputs;

        protected virtual WindowInfo WindowInfo => new()
        {
            Title = "Pyrite",
            BackgroundColor = Color.Black,

#if PS4 || XBOXONE
            Size = new(1920, 1080),
            Maximized = true,
            Resizable = false,
#elif NSWITCH
            Size = new(1080, 720),
            Maximized = true,
            Resizable = false,
#else
            Size = new(1080, 720),
            MinimalSize = new(720, 480),
            Maximized = false,
            Resizable = true,
#endif
        };

        private float _timeUntilFixedUpdate = 0f;

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

            _timeUntilFixedUpdate = Time.FixedDeltaTime;
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

        /// <summary>
        /// Called on game window load
        /// </summary>
        private void OnLoad()
        {
#if DEBUG
            Console.WriteLine("Start Game Initialization");
#endif
            Initialize();
            PercistentWorld.Start();
            SceneManager.CurrentScene.Start();
#if DEBUG
            Console.WriteLine("Finished Game Initialization");
#endif
        }

        /// <summary>
        /// Called every game window update.
        /// Manage a fixed update time.
        /// </summary>
        private void OnUpdate(double deltaTime)
        {
            Time.Update(deltaTime);

            PercistentWorld.Update();
            SceneManager.CurrentScene.Update();

            // manage fixed update 
            _timeUntilFixedUpdate -= Time.DeltaTime;
            if (_timeUntilFixedUpdate <= 0f) // todo : check if there can be a frame skip ? does it matter ? 
            {
                _timeUntilFixedUpdate += Time.FixedDeltaTime; // += to ensure a consistant fixed update time 
                PercistentWorld.FixedUpdate();
                SceneManager.CurrentScene.FixedUpdate();
            }
        }

        /// <summary>
        /// Called every game window render 
        /// </summary>
        private void OnRender()
        {
            _percistentWorld?.Render();
            SceneManager.CurrentScene.Render();
        }

        private void OnClose()
        {
#if DEBUG
            Console.WriteLine("Close Game");
#endif
            PercistentWorld.Exit();
            SceneManager.CurrentScene.Exit();
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