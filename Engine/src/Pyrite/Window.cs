﻿using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Input;

using System.Drawing;

using SilkWindow = Silk.NET.Windowing.Window;
using Icon = Pyrite.Core.Graphics.Icon;
using Silk.NET.OpenGL;

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
        public event Action<double>? OnRender;
        public event Action? OnClose;

        public int Width;
        public int Height;

        private readonly Icon _icon;
        public Color BackgroundColor;

        public Window(WindowInfo info)
        {
            Width = info.Width;
            Height = info.Height;

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(Width, Height);
            options.Title = info.Title;
            options.WindowBorder = info.Resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
            options.WindowState = info.Maximized ? WindowState.Maximized : WindowState.Normal;

            BackgroundColor = info.BackgroundColor;
            _icon = string.IsNullOrEmpty(info.IconPath) ? Icon.Default : new(info.IconPath);

            _native = SilkWindow.Create(options);

            // Set events
            _native.Load += OnLoad;
            _native.Load += () =>
            {

                // todo : Create input system instance and set appropriate callbacks
                //Set-up input context.
                IInputContext input = _native.CreateInput();
                for (int i = 0; i < input.Keyboards.Count; i++)
                {
                    input.Keyboards[i].KeyDown += KeyDown;
                }

                _native.Center();
                if (_icon is not null)
                {
                    var raw = _icon.Raw;
                    _native.SetWindowIcon(ref raw);
                }
            };

            _native.Update += OnUpdate;
            _native.Render += OnRender;
            _native.Closing += OnClose;

            try
            {
                _native.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _native.Dispose();
        }

        private void KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            //Check to close the window on escape.
            if (key == Key.Escape)
            {
                _native.Close();
            }
        }

        internal IWindow Native() => _native;

        public void Run() => _native.Run();

        public void Close() => _native.Close();

        public void Dispose()
        {
            OnLoad = null;
            OnUpdate = null;
            OnRender = null;
            OnClose = null;
        }
    }
}
