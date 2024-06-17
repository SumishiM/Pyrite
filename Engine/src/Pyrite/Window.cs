

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
            _native.ClientSizeChanged += (s, e) => { RefreshWindow();};
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
                if (_info.WindowedSize.X > _info.MinimalWindowedSize.X && _info.WindowedSize.Y > _info.MinimalWindowedSize.Y)
                {
                    // set window info size
                    _graphics.PreferredBackBufferWidth = _info.WindowedSize.X;
                    _graphics.PreferredBackBufferHeight = _info.WindowedSize.Y;
                }
                else
                {
                    // set custom size
                    _graphics.PreferredBackBufferWidth = size.X;
                    _graphics.PreferredBackBufferHeight = size.Y;
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
        }

    }
}