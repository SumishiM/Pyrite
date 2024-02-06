using Ignite;
using Pyrite.Core;
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

        //        //public static Commands Commands { get; private set; }
        //        //public static Pooler Pooler { get; private set; }
        //        public static Action? OverloadGameLoop;

        //        // screen size
        //        public static int Width { get; private set; }
        //        public static int Height { get; private set; }
        //        public static int ViewWidth { get; protected set; }
        //        public static int ViewHeight { get; protected set; }
        //        public static int ViewPadding
        //        {
        //            get { return viewPadding; }
        //            set
        //            {
        //                viewPadding = value;
        //                Instance.UpdateView();
        //            }
        //        }
        //        private static int viewPadding = 0;
        //        private static bool resizing;


        //        public Game(int width, int height, int windowWidth, int windowHeight, string windowTitle, bool fullscreen)
        //        {
        //            Instance = this;

        //            Title = Window.Title = windowTitle;
        //            Width = width;
        //            Height = height;
        //            ClearColor = Color.Black;

        //#if PS4 || XBOXONE
        //            Graphics.PreferredBackBufferWidth = 1920;
        //            Graphics.PreferredBackBufferHeight = 1080;
        //#elif NSWITCH
        //            Graphics.PreferredBackBufferWidth = 1280;
        //            Graphics.PreferredBackBufferHeight = 720;
        //#else
        //            Window.AllowUserResizing = true;
        //            Window.ClientSizeChanged += OnClientSizeChanged;

        //            if (fullscreen)
        //            {
        //                Graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
        //                Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
        //                Graphics.IsFullScreen = true;
        //            }
        //            else
        //            {
        //                Graphics.PreferredBackBufferWidth = windowWidth;
        //                Graphics.PreferredBackBufferHeight = windowHeight;
        //                Graphics.IsFullScreen = false;
        //            }
        //#endif
        //            Graphics.ApplyChanges();

        //            Content.RootDirectory = @"Content";
        //            if( !Directory.Exists(Content.RootDirectory))
        //                Directory.CreateDirectory(Content.RootDirectory);

        //            IsMouseVisible = false;
        //            IsFixedTimeStep = false;

        //            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        //        }

        //#if !CONSOLE
        //        protected virtual void OnClientSizeChanged(object? sender, EventArgs e)
        //        {
        //            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0 && !resizing)
        //            {
        //                resizing = true;

        //                Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        //                Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        //                UpdateView();

        //                resizing = false;
        //            }
        //        }
        //#endif

        //        protected virtual void OnGraphicsReset(object? sender, EventArgs e)
        //        {
        //            UpdateView();
        //        }

        //        protected virtual void OnGraphicsCreate(object? sender, EventArgs e)
        //        {
        //            UpdateView();
        //        }

        //        protected override void OnActivated(object sender, EventArgs args)
        //        {
        //            base.OnActivated(sender, args);
        //        }

        //        protected override void OnDeactivated(object sender, EventArgs args)
        //        {
        //            base.OnDeactivated(sender, args);
        //        }

        //        protected override void Initialize()
        //        {
        //            base.Initialize();
        //        }

        //        protected override void LoadContent()
        //        {
        //            base.LoadContent();
        //        }

        //        protected override void Update(GameTime gameTime)
        //        {
        //            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        //            DeltaTime = RawDeltaTime * TimeRate;

        //            if (OverloadGameLoop != null)
        //            {
        //                OverloadGameLoop();
        //                base.Update(gameTime);
        //                return;
        //            }

        //            base.Update(gameTime);
        //        }

        //        protected override void Draw(GameTime gameTime)
        //        {
        //            RenderCore();

        //            base.Draw(gameTime);

        //            //Frame counter
        //            fpsCounter++;
        //            counterElapsed += gameTime.ElapsedGameTime;
        //            if (counterElapsed >= TimeSpan.FromSeconds(1))
        //            {
        //#if DEBUG
        //                Window.Title = Title + " - " + fpsCounter.ToString() + " fps - " + (GC.GetTotalMemory(false) / 1048576f).ToString("F") + " MB";
        //#else
        //                Window.Title = Title;
        //#endif
        //                FPS = fpsCounter;
        //                fpsCounter = 0;
        //                counterElapsed -= TimeSpan.FromSeconds(1);
        //            }
        //        }

        //        /// <summary>
        //        /// Override if you want to change the core rendering functionality of Monocle Engine.
        //        /// By default, this simply sets the render target to null, clears the screen, and renders the current Scene
        //        /// </summary>
        //        protected virtual void RenderCore()
        //        {
        //            GraphicsDevice.SetRenderTarget(null);
        //            GraphicsDevice.Viewport = Viewport;
        //            GraphicsDevice.Clear(new Color(ClearColor.R, ClearColor.G, ClearColor.B, 0.6f));
        //        }

        //        protected override void OnExiting(object sender, EventArgs args)
        //        {
        //            base.OnExiting(sender, args);
        //        }

        //        public void RunWithLogging()
        //        {
        //            try
        //            {
        //                Run();
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //            }
        //        }


        //        #region Screen

        //        public static Viewport Viewport { get; protected set; }
        //        public static Matrix ScreenMatrix;

        //        public static void SetWindowed(int width, int height)
        //        {
        //#if !CONSOLE
        //            if (width > 0 && height > 0)
        //            {
        //                resizing = true;
        //                Graphics.PreferredBackBufferWidth = width;
        //                Graphics.PreferredBackBufferHeight = height;
        //                Graphics.IsFullScreen = false;
        //                Graphics.ApplyChanges();
        //                Console.WriteLine("WINDOW-" + width + "x" + height);
        //                resizing = false;
        //            }
        //#endif
        //        }

        //        public static void SetFullscreen()
        //        {
        //#if !CONSOLE
        //            resizing = true;
        //            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        //            Graphics.IsFullScreen = true;
        //            Graphics.ApplyChanges();
        //            Console.WriteLine("FULLSCREEN");
        //            resizing = false;
        //#endif
        //        }

        //        private void UpdateView()
        //        {
        //            float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        //            float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        //            // get View Size
        //            if (screenWidth / Width > screenHeight / Height)
        //            {
        //                ViewWidth = (int)(screenHeight / Height * Width);
        //                ViewHeight = (int)screenHeight;
        //            }
        //            else
        //            {
        //                ViewWidth = (int)screenWidth;
        //                ViewHeight = (int)(screenWidth / Width * Height);
        //            }

        //            // apply View Padding
        //            var aspect = ViewHeight / (float)ViewWidth;
        //            ViewWidth -= ViewPadding * 2;
        //            ViewHeight -= (int)(aspect * ViewPadding * 2);

        //            // update screen matrix
        //            ScreenMatrix = Matrix.CreateScale(ViewWidth / (float)Width);

        //            // update viewport
        //            Viewport = new Viewport
        //            {
        //                X = (int)(screenWidth / 2 - ViewWidth / 2),
        //                Y = (int)(screenHeight / 2 - ViewHeight / 2),
        //                Width = ViewWidth,
        //                Height = ViewHeight,
        //                MinDepth = 0,
        //                MaxDepth = 1
        //            };

        //            //Debug Log
        //            //Calc.Log("Update View - " + screenWidth + "x" + screenHeight + " - " + viewport.Width + "x" + viewport.GuiHeight + " - " + viewport.X + "," + viewport.Y);
        //        }

        //        #endregion
    }
}