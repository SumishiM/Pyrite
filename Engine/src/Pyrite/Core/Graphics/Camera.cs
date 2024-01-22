using Ignite.Components;
using System.Numerics;

namespace Pyrite.Core.Graphics
{
    public class Camera : IComponent
    {
        private readonly Window _window;

        public float Zoom { get; set; } = 1f;

        public Camera(Window window)
        {
            _window = window;
        }

        public Vector2 Position { get; set; }

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
    }
}
