

using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Core.Geometry;

namespace Pyrite
{

    public struct WindowInfo
    {
        /// <summary>
        /// Window size while windowed
        /// </summary>
        public Point WindowedSize;

        /// <summary>
        /// Minimal window size while windowed
        /// </summary>
        public Point MinimalWindowedSize;

        /// <summary>
        /// Whether the window is resizable or not
        /// </summary>
        public bool Resizable;

        public static WindowInfo Default => new()
        {
            WindowedSize = new(1080, 720),
            MinimalWindowedSize = new(1080, 720),
            Resizable = true
        };
    }

    public class Window
    {
        private static Window? _instance;

        protected WindowInfo _info;
        private readonly Microsoft.Xna.Framework.GameWindow _native;
        private readonly Microsoft.Xna.Framework.GraphicsDeviceManager _graphics;
        internal Microsoft.Xna.Framework.GameWindow Native => _native;

        protected bool _isFullscreen;
        public static bool IsFullscreen
        {
            get => _instance?._isFullscreen ?? false;
            set
            {
                Debug.Assert(_instance is null, "Window is null !");
                _instance!._isFullscreen = value;
                _instance!.RefreshWindow();
            }
        }

        public Vector2 GameScale
        {
            get
            {
                if (_native.ClientBounds.Width <= 0 || _native.ClientBounds.Height <= 0)
                    return Vector2.One;

                return new(
                    Game.Settings.GameWidth / (float)_native.ClientBounds.Width,
                    Game.Settings.GameHeight / (float)_native.ClientBounds.Height);
            }
        }

        public int Width => _native.ClientBounds.Width;
        public int Height => _native.ClientBounds.Height;
        public Point Size => new(_native.ClientBounds.Width, _native.ClientBounds.Height);
        public Matrix ScreenMatrix = Matrix.Identity;
        public Viewport Viewport;

        public Window(
            IPyriteGame game,
            Microsoft.Xna.Framework.GameWindow native,
            ref Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            _instance = this;

            _native = native;
            _graphics = graphics;
            _info = game.GameWindowInfo;

            _native.Title = game.Name;
#if DEBUG
            _native.Title += $" | v{game.Version}";
#endif
            _native.AllowUserResizing = _info.Resizable;
            _native.ClientSizeChanged += OnClientSizeChanged;
            
        }

        protected virtual void OnClientSizeChanged(object? sender, EventArgs e)
        {
            if (_native.ClientBounds.Width > 0 && _native.ClientBounds.Height > 0)
            {
                _graphics.PreferredBackBufferWidth = _native.ClientBounds.Width;
                _graphics.PreferredBackBufferHeight = _native.ClientBounds.Height;
                UpdateView();
            }
        }

        internal void UpdateView()
        {
            float screenWidth = _graphics.PreferredBackBufferWidth;
            float screenHeight = _graphics.PreferredBackBufferHeight;

            int viewWidth = 0;
            int viewHeight = 0;

            if (screenHeight / Game.Settings.GameWidth > screenHeight / Game.Settings.GameHeight)
            {
                viewWidth = (int)(screenHeight / Game.Settings.GameHeight * Game.Settings.GameWidth);
                viewHeight = (int)screenHeight;
            }
            else
            {
                viewWidth = (int)screenWidth;
                viewHeight = (int)(screenWidth / Game.Settings.GameWidth * Game.Settings.GameHeight);
            }

            var aspect = viewHeight / viewWidth;
            ScreenMatrix = Microsoft.Xna.Framework.Matrix.CreateScale(viewWidth / (float)Game.Settings.GameWidth);

            Viewport = new Viewport()
            {
                X = (int)(screenWidth / 2f - viewWidth / 2f),
                Y = (int)(screenHeight / 2f - viewHeight / 2f),
                Width = viewWidth,
                Height = viewHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
        } 

        protected virtual void SetWindowSize(Point size)
        {
            if (IsFullscreen)
            {
                // collect fullscreen size
                _info.WindowedSize = new(_graphics.GraphicsDevice.Viewport.Bounds.Width, _graphics.GraphicsDevice.Viewport.Bounds.Height);

                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                if (_native.ClientBounds.Width > _info.MinimalWindowedSize.X)
                {
                    // set window info size
                    _info.WindowedSize.X = _native.ClientBounds.Width;
                    _graphics.PreferredBackBufferWidth = _info.WindowedSize.X;
                }
                else
                {
                    // set custom size
                    _graphics.PreferredBackBufferWidth = _info.MinimalWindowedSize.X;
                }

                if (_native.ClientBounds.Height > _info.MinimalWindowedSize.Y)
                {
                    _info.WindowedSize.Y = _native.ClientBounds.Height;
                    _graphics.PreferredBackBufferHeight = _info.WindowedSize.Y;
                }
                else
                {
                    _graphics.PreferredBackBufferHeight = _info.MinimalWindowedSize.Y;
                }
            }

            _graphics.ApplyChanges();

            // set window according to fullscreen settings
            _graphics.IsFullScreen = IsFullscreen;
            _native.IsBorderlessEXT = IsFullscreen;
#if DEBUG
            _graphics.SynchronizeWithVerticalRetrace = IsFullscreen;
#endif
        }

        internal virtual void RefreshWindow()
        {
            SetWindowSize(new(Game.Settings.GameWidth, Game.Settings.GameHeight));
            ScreenMatrix = Microsoft.Xna.Framework.Matrix.CreateScale(_graphics.GraphicsDevice.Viewport.Bounds.Width / (float)Game.Settings.GameWidth);
        }

    }
}