using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Core.Graphics;

namespace Pyrite.Components.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public class CameraComponent : IComponent
    {
        private readonly Camera _camera;

        public CameraComponent()
        {
            _camera = new(Game.Window.Width, Game.Window.Height);
        }

        public CameraComponent(int width, int height)
        {
            _camera = new(width, height);
        }

        public CameraComponent(Camera camera)
        {
            _camera = camera;
        }

        public static implicit operator CameraComponent(Camera camera) => new(camera);
        public static implicit operator Camera(CameraComponent component) => component._camera;
    }
}
