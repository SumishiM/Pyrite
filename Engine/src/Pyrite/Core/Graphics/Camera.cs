using Ignite;
using Ignite.Extentions;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Utils;
using System.Collections.Immutable;

namespace Pyrite.Core.Graphics
{
    public class Camera
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle Bounds { get; private set; }
        public Rectangle SafeBounds { get; private set; }

        private readonly Vector2 _origin = Vector2.Zero;
        private Transform _transform;
        private float _zoom = 1;

        private bool _locked;


        private Matrix? _cachedWorldViewProjection;
        public Matrix WorldViewProjection
        {
            get
            {
                _cachedWorldViewProjection ??= GetWorldView();

                return _cachedWorldViewProjection.Value;
            }
        }

        public void ClearCache()
        {
            _cachedWorldViewProjection = null;
        }

        public Vector2 ConvertWorldToScreenPosition(Vector2 position, Point viewportSize)
        {
            Vector2 scale = new Vector2(Width * 1f / viewportSize.X, Height * 1f / viewportSize.Y);
            return WorldToScreenPosition(position * scale);
        }

        public float Zoom
        {
            get => _zoom;
            set
            {
                float zoom = Math.Clamp(value, 0.1f, 512f);

                if (zoom != _zoom)
                {
                    _zoom = zoom;
                    _cachedWorldViewProjection = null;
                }
            }
        }

        public int HalfWidth => Calculator.RoundToInt(Width / 2f);

        public Point Size => new(Width, Height);

        public float Aspect => Width / (float)Height;

        public Camera(int width, int height)
        {
            Width = width; 
            Height = height;

            _main ??= this;
            _transform = Transform.Empty;

            // Origin will be the center of the camera.
            _origin = new Vector2(0.5f, 0.5f);
        }

        internal static Camera? _main;
        public static Camera Main
        {
            get
            {
                if (_main is null || _main?._transform is null)
                {
                    // should never be null while the scene is running
                    var node = SceneManager.CurrentScene.World
                        .GetNodesWith(typeof(CameraComponent))
                        .FirstOrDefault(n => n.Name == "Main Camera" /*Todo : compare with tag*/) 
                        ?? 
                        throw new NullReferenceException(
                            "Camera.Main is null, is the simulation world loaded ? " +
                            "or is there no CameraComponent in the world ?");

                    _main = node.GetComponent<CameraComponent>();
                    _main._transform = node.GetComponent<TransformComponent>();
                }

                return _main;
            }
        }

        public Vector2 ScreenToWorldPosition(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(WorldViewProjection));
        }

        public Vector2 WorldToScreenPosition(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, WorldViewProjection);
        }

        internal void UpdateSize(int width, int height)
        {
            Width = Math.Max(1, width);
            Height = Math.Max(1, height);

            _cachedWorldViewProjection = null;
        }

        private Matrix GetWorldView()
        {
            Point position = _transform.Position;
            Point center = (_origin * new Vector2(Width, Height));

            // First, let's start with our initial position.
            Matrix view = Matrix.CreateTranslation(
                x: -position.X,
                y: -position.Y,
                z: 0);

            // Now, overcompensate the origin by changing our relative position.
            // This will make sure we are ready for any rotation and scale operations
            // with the correct relative position.
            view *= Matrix.CreateTranslation(
                x: -center.X,
                y: -center.Y,
                z: 0);

            // Now, we will apply the scale operation.
            view *= Matrix.CreateRotationZ(_transform.Rotation.ToRadians());

            // And our zoom!
            view *= Matrix.CreateScale(_zoom, _zoom, 1);

            // Okay, we are done. Now go back to our correct position.
            view *= Matrix.CreateTranslation(
                x: center.X,
                y: center.Y,
                z: 0);

            var inverseMatrix = Matrix.Invert(view);
            var topLeftCorner = Vector2.Transform(new Vector2(0, 0), inverseMatrix);
            var bottomRightCorner = Vector2.Transform(new Vector2(Width, Height), inverseMatrix);

            Matrix orthographicMatrix = System.Numerics.Matrix4x4.CreateOrthographicOffCenter(
                topLeftCorner.X, bottomRightCorner.X, bottomRightCorner.Y, topLeftCorner.Y, 0.01f, 100f);

            Bounds = new Rectangle(topLeftCorner, (bottomRightCorner - topLeftCorner));
            //SafeBounds = Bounds.Expand(Grid.CellSize * 2);
            return orthographicMatrix;
        }

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
        }

        internal void Reset()
        {
            Unlock();
            _transform.Position = Vector2.Zero;
            Zoom = 1;
        }
    }
}
