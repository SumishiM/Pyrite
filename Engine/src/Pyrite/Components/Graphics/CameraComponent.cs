using Ignite.Attributes;
using System.Numerics;

namespace Pyrite.Components.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public class CameraComponent : Component
    {
        private readonly Window _window;

        public Vector2 Position { get; set; } // parent.Transform ? 
        private Vector2 _initialSize { get; set; }
        public float Zoom { get; set; } = 1f;

        internal Matrix4x4 ProjectionMatrix
        {
            get
            {
                // change with résolution system
                var sizeRatio = new Vector2(_window.Width, _window.Height) / _initialSize;
                var left = Position.X - _window.Width / 2f;
                var right = Position.X + _window.Width / 2f;
                var top = Position.Y + _window.Height / 2f;
                var bottom = Position.Y - _window.Height / 2f;

                var orthographicMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
                var zoomMatrix = Matrix4x4.CreateScale(new Vector3  (sizeRatio.X * Zoom, sizeRatio.Y * Zoom, 1f));
                return orthographicMatrix * zoomMatrix;
            }
        }

        public CameraComponent()
        {
            _window = Game.Window;
            _initialSize = new Vector2(Game.Window.Width, Game.Window.Height);
        }

        public CameraComponent ( Window window )
        {
            _window = window;
            _initialSize = new Vector2(Game.Window.Width, Game.Window.Height);
        }
    }
}
