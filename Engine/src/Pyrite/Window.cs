using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Input;

using System.Drawing;

using SilkWindow = Silk.NET.Windowing.Window;
using Icon = Pyrite.Graphics.Icon;

namespace Pyrite
{
    public struct WindowInfo
    {
        public string Title;
        public int Width;
        public int Height;
        public bool Resizable;
        public bool Maximized;
        public Color BackgroundColor;
        public string? IconPath;
    }

    public class Window : IDisposable
    {
        private readonly IWindow _native;

        public event Action? OnLoad;
        public event Action<double>? OnUpdate;
        public event Action? OnRender;
        public event Action? OnClose;

        public int Width;
        public int Height;

        private readonly Icon _icon;
        public static Color BackgroundColor { get; private set; } = Color.Black;


        public Window ( WindowInfo info )
        {
            Width = info.Width;
            Height = info.Height;

#if DEBUG
            Console.WriteLine($"Create window [{Width}, {Height}]");
#endif

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(Width, Height);
            options.Title = info.Title;
            options.WindowBorder = info.Resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
            options.WindowState = info.Maximized ? WindowState.Maximized : WindowState.Normal;

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
                IInputContext input = _native.CreateInput();
                for ( int i = 0; i < input.Keyboards.Count; i++ )
                {
                    input.Keyboards[i].KeyDown += KeyDown;
                }

                _native.Center();
                if ( _icon is not null )
                {
                    var raw = _icon.Raw;
                    _native.SetWindowIcon(ref raw);
                }

                OnLoad?.Invoke();
            };

            _native.Update += ( dt ) => OnUpdate?.Invoke(dt);
            _native.Render += _ => OnRender?.Invoke();
            _native.Closing += () => OnClose?.Invoke();
            _native.Resize += OnResize;
        }

        private void OnResize ( Vector2D<int> d )
        {
            Width = d.X;
            Height = d.Y;
        }

        private void KeyDown ( IKeyboard keyboard, Key key, int arg3 )
        {
            //Check to close the window on escape.
            if ( key == Key.Escape )
            {
                _native.Close();
            }
            if ( key == Key.P )
            {
                if ( Game.Instance.PercistentWorld.IsPaused )
                {
                    Game.Instance.PercistentWorld.Resume();
#if DEBUG
                    Console.WriteLine("Resume");
#endif
                }
                else
                {
                    Game.Instance.PercistentWorld.Pause();
#if DEBUG
                    Console.WriteLine("Pause");
#endif
                }
            }
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
