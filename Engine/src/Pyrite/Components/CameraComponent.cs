using Ignite.Attributes;
using Pyrite.Components;
using System.Numerics;

namespace Pyrite.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public class CameraComponent : Component
    {
        private readonly Window _window;

        public Vector2 Position { get; set; } // parent.Transform ? 
        public float Zoom { get; set; } = 1f;

        internal Matrix4x4 ProjectionMatrix
        {
            get
            {
                var left = Position.X - _window.Width / 2f;
                var right = Position.X + _window.Width / 2f;
                var top = Position.Y + _window.Height / 2f;
                var bottom = Position.Y - _window.Height / 2f;

                var orthographicMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
                var zoomMatrix = Matrix4x4.CreateScale(Zoom);
                return orthographicMatrix * zoomMatrix;
            }
        }

        public CameraComponent()
        {
            _window = Game.Window;
        }

        public CameraComponent ( Window window )
        {
            _window = window;
        }
    }

    public static class Camera
    {
        private static CameraComponent? _main = null;
        public static CameraComponent Main
        {
            get
            {
                _main ??= new();
                return _main;
            }
        }
    }
}
