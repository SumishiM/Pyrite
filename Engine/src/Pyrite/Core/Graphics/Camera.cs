using Pyrite.Core.Geometry;

namespace Pyrite.Core.Graphics
{
    public class Camera
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle Bounds { get; private set; }
        public Rectangle SafeBounds { get; private set; }

        private readonly Vector2 _origin = Vector2.Zero;

        private Transform Transform;

        private float _rotation = 0f;
        private float _zoom = 1f;

        //private Matrix4x4? _cachedWorldViewProjection;

        //public Matrix4x4 WorldViewProjection
        //{
        //    get
        //    {
        //        _cachedWorldViewProjection ??= GetWorldView();
        //        return _cachedWorldViewProjection.Value;
        //    }
        //}

        public Camera(Transform transform)
        {
            Transform = transform;
        }

        public Point GetCursorWorldPosition()
        {
            return Point.Zero;
        }

        //public Matrix4x4 GetWorldView() {
        //    Point position = Transform.Position;
        //    Point center = _origin * new Vector2(Width, Height).;
        //}
    }
}
