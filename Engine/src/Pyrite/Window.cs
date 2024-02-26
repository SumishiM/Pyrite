using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Input;

using SilkWindow = Silk.NET.Windowing.Window;
using Icon = Pyrite.Core.Graphics.Icon;

using Pyrite.Core.Graphics;
using Pyrite.Core.Inputs;
using Pyrite.Core.Geometry;

namespace Pyrite
{
    public struct WindowInfo
    {
        public string Title;
        public Point Size;
        public Point MinimalSize;
        public bool Resizable;
        public bool Maximized;
        public System.Drawing.Color BackgroundColor;
        public string? IconPath;
    }

    public class Window : IDisposable
    {
        private readonly IWindow _native;

        public event Action? OnLoad;
        public event Action<double>? OnUpdate;
        public event Action? OnRender;
        public event Action? OnClose;

        public int Width => _native.FramebufferSize.X;
        public int Height => _native.FramebufferSize.Y;

        private readonly Icon _icon;
        public static System.Drawing.Color BackgroundColor { get; private set; } = System.Drawing.Color.Black;


        public Window ( WindowInfo info )
        {

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(info.Size.X, info.Size.Y);
            options.Title = info.Title;
            options.WindowBorder = info.Resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
            options.WindowState = info.Maximized ? WindowState.Maximized : WindowState.Normal;
            options.ShouldSwapAutomatically = true;

#if DEBUG
            Console.WriteLine($"Create window {options.Size}");
#endif
            BackgroundColor = info.BackgroundColor;
            _icon = string.IsNullOrEmpty(info.IconPath) ? Icon.Default : new(info.IconPath);

            _native = SilkWindow.Create(options);

            // Set events
            _native.Load += () =>
            {
#if DEBUG
                Console.WriteLine($"OnLoad window");
#endif
                // todo : Create input system instance and set appropriate callbacks
                //Set-up input context.
                Input.Initialize(_native.CreateInput());

                _native.Center();
                if ( _icon is not null )
                {
                    var raw = _icon.Raw;
                    _native.SetWindowIcon(ref raw);
                }

                OnLoad?.Invoke();
            };

            _native.Update += deltaTime => OnUpdate?.Invoke(deltaTime);
            _native.Render += _ => OnRender?.Invoke();
            _native.Closing += () => OnClose?.Invoke();

            _native.Resize += s =>
            {
                var position = _native.Position;
                if (s.X < info.MinimalSize.X)
                {
                    _native.Size = new Vector2D<int>(info.MinimalSize.X, s.Y);
                }
                if (s.Y < info.MinimalSize.Y)
                {
                    _native.Size = new Vector2D<int>(s.X, info.MinimalSize.Y);
                }
                _native.Position = position;
            };

            // Handle resizes
            _native.FramebufferResize += s =>
            {
                var position = _native.Position;
                if (_native.Size.X > info.MinimalSize.X && _native.Size.Y > info.MinimalSize.Y)
                {
                    // add if OpenGL
                    Core.Graphics.Graphics.Gl.Viewport(s);
                    Camera.Main.UpdateSize(s.X, s.Y);
                }
                _native.Position = position;
            };
        }

        internal IWindow Native () => _native;

        public void Run ()
        {
#if DEBUG
            Console.WriteLine($"Run window");
#endif
            _native.Run();
        }

        public void Close ()
        {
#if DEBUG
            Console.WriteLine($"Close window");
#endif
            _native.Close();
        }

        public void Dispose ()
        {
            OnLoad = null;
            OnUpdate = null;
            OnRender = null;
            OnClose = null;

            _native.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
