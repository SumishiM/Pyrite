﻿using Ignite;
using Pyrite.Assets;
using Pyrite.Core;
using Pyrite.Core.Inputs;
using Pyrite.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Pyrite.Core.Graphics;
using System.Collections.Specialized;

namespace Pyrite
{
    public partial class Game : Microsoft.Xna.Framework.Game
    {
        private static Game? _instance = null;
        /// <summary>
        /// Game instance
        /// </summary>
        public static Game Instance
        {
            get
            {
                Debug.Assert(_instance is not null, $"Game.Instance is null! Try get the instance after the game is initialized.");
                return _instance!;
            }
        }

        private readonly IPyriteGame? _game;
        private readonly GameSettings _settings = new();
        public static GameSettings Settings => _instance!._settings;

        protected readonly Microsoft.Xna.Framework.GraphicsDeviceManager _graphicsDeviceManager;

        public static new GraphicsDevice GraphicsDevice => Instance._graphicsDeviceManager.GraphicsDevice;


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
                    _defaultWorldBuilder.AddSystems([.. _game?.PercistentSystems ?? []]);
                    _percistentWorld = _defaultWorldBuilder.Build();
#if DEBUG
                    Console.WriteLine($"Percistent world built with {(_game?.PercistentSystems ?? []).Count} systems.");
#endif
                }
                return _percistentWorld;
            }
        }

        /// <summary>
        /// Input system instance
        /// </summary>
#nullable disable
        public InputContextMapping Inputs;
#nullable enable 

        /// <summary>
        /// Game asset database instance
        /// </summary>
        public static AssetDatabase Data => _assetDatabase;
        private static readonly AssetDatabase _assetDatabase = new();

        private readonly Window _window;
        public new Window Window => _window;

#nullable disable
        public SpriteBatch SpriteBatch;
#nullable enable

        private float _timeUntilFixedUpdate = 0f;

        public Game(IPyriteGame game)
        {
#if DEBUG
            var startTime = new Stopwatch();
            startTime.Restart();
            Console.WriteLine($"Creating {_game?.Name ?? "Pyrite Application"}.");
#endif
            _instance = this;
            _game = game;
            _graphicsDeviceManager = new(this);

            _window = new(
                _game,
                base.Window,
                ref _graphicsDeviceManager
            );
            _window.UpdateView();

            _ = new Camera(Settings.GameWidth, Settings.GameHeight);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            Time.FixedDeltaTime = 1f / (_settings.TargetFPS / _settings.FixedUpdateFactor);
            _timeUntilFixedUpdate = Time.FixedDeltaTime;

#if DEBUG
            Console.WriteLine($"{_game?.Name ?? "Pyrite Application"} created in {startTime.ElapsedMilliseconds}ms.");
#endif
        }

        protected override void Initialize()
        {
#if DEBUG
            var startTime = new Stopwatch();
            startTime.Restart();
            Console.WriteLine($"Initializing...");
#endif
            base.Initialize();
            Data.Initialize();
            _game?.Initialize();

#if DEBUG
            Console.WriteLine($"Starting IgniteECS Worlds.");
#endif

            // start ECS
            PercistentWorld.Start();
            SceneManager.CurrentScene.Start();

#if DEBUG
            Console.WriteLine($"Initialized in {startTime.ElapsedMilliseconds}ms.");
#endif
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            SpriteBatch = new(GraphicsDevice);

            Data.TryAddAsset(new TextureAsset("Content/toothless.png"));
            

            _game?.LoadContent();
            //_game?.LoadContentAsync();
        }

        /// <summary>
        /// Called every game window update.
        /// Manage a fixed update time.
        /// </summary>
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // update time
            Time.Update(gameTime.ElapsedGameTime.TotalSeconds);

            // update ECS
            PercistentWorld.Update();
            SceneManager.CurrentScene.Update();

            // handle fixed update 
            _timeUntilFixedUpdate -= Time.DeltaTime;
            if (_timeUntilFixedUpdate <= 0f) // todo : check if there can be a frame skip ? does it matter ? 
            {
                _timeUntilFixedUpdate += Time.FixedDeltaTime; // += to ensure a consistant fixed update time 
                // fixed update ECS
                PercistentWorld.FixedUpdate();
                SceneManager.CurrentScene.FixedUpdate();
            }
        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            //GraphicsDevice.SetRenderTarget(new(GraphicsDevice, Settings.GameWidth, Settings.GameHeight));

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Viewport = Window.Viewport;
            GraphicsDevice.Clear(Color.Black);

            SceneManager.CurrentScene.Render();
            
            _game?.OnDraw();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Console.WriteLine("Exiting !");
        }

        protected override void Dispose(bool disposing)
        {
            SceneManager.CurrentScene.Dispose();
            PercistentWorld.Dispose();

            base.Dispose(disposing);
        }
    }

    public class GameSettings
    {

        public readonly int GameWidth = 320;
        public readonly int GameHeight = 180;
        public readonly int TargetFPS = 60;
        public readonly float FixedUpdateFactor = 2f;

        public readonly string AsepriteFolderPath = "aseprite/";
        public readonly string AssetsFolderPath = "assets/";
        public readonly string AtlasFolderPath = "atlas/";
        public readonly string ConfigFolderPath = "config/";
        public readonly string ECSFolderPath = "ecs/";
        public readonly string FontFolderPath = "fonts/";
        public readonly string ShaderFolderPath = "shaders/";
        public readonly string SoundsFolderPath = "sounds/";

        public readonly string GenericAssetsFolderPath = "data/";
    }
}