using Microsoft.Xna.Framework.Graphics;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    public class Camera
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle Bounds { get; private set; }
        public Rectangle SafeBounds { get; private set; }

        private readonly Vector2 _origin = Vector2.Zero;

        protected Transform _transform;
        public Transform Transform
        {
            get => _transform;
            set
            {
                _transform = value;
                _cachedWorldViewProjection = null;
            }
        }

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
        public Viewport Viewport;

        public Camera(int width, int height)
        {
            Width = width;
            Height = height;

            _main ??= this;
            _transform = Transform.Empty;

            // Origin will be the center of the camera.
            _origin = new Vector2(0.5f, 0.5f);

            Viewport = new()
            {
                Width = width,
                Height = height
            };

            _cachedWorldViewProjection = null;
        }

        public float Top => Vector2.Transform(Vector2.Zero, Matrix.Invert(WorldViewProjection)).Y;
        public float Bottom => Vector2.Transform(Vector2.UnitY * Viewport.Height, Matrix.Invert(WorldViewProjection)).Y;
        public float Left => Vector2.Transform(Vector2.Zero, Matrix.Invert(WorldViewProjection)).X;
        public float Right => Vector2.Transform(Vector2.UnitX * Viewport.Width, Matrix.Invert(WorldViewProjection)).X;

        internal static Camera? _main;
        public static Camera Main
        {
            get
            {
                if (_main is null)
                {
                    _main = new Camera(Game.Settings.GameWidth, Game.Settings.GameHeight);
                }
                return _main;
                if (_main is null || _main?.Transform is null)
                {
                    // should never be null while the scene is running
                    var node = SceneManager.CurrentScene.World
                        .GetNodesWith(typeof(CameraComponent))
                        .FirstOrDefault(n => n.Name == "Main Camera" /*Todo : compare with tag ?*/)
                        ??
                        throw new NullReferenceException(
                            "Camera.Main is null, is the simulation world loaded ? " +
                            "or is there no CameraComponent in the world ?");

                    _main = node.GetComponent<CameraComponent>();
                    _main.Transform = node.GetComponent<TransformComponent>();
                }

                return _main;
            }
        }

        public Vector2 ScreenToWorldPosition(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(WorldViewProjection));
        }

        public Vector2 WorldToScreenPosition(Vector3 screenPosition)
        {
            return Vector2.Transform(screenPosition, WorldViewProjection);
        }

        internal void UpdateSize(int width, int height)
        {
            Width = Math.Max(1, width);
            Height = Math.Max(1, height);

            _cachedWorldViewProjection = null;
        }

        internal void UpdateSize(Point size)
        {
            Width = Math.Max(1, size.X);
            Height = Math.Max(1, size.Y);

            _cachedWorldViewProjection = null;
        }

        private Matrix GetWorldView()
        {
            return Microsoft.Xna.Framework.Matrix.Identity *
                    Microsoft.Xna.Framework.Matrix.CreateTranslation(new Vector3(-new Vector2(MathF.Floor(Transform.Position.X), MathF.Floor(Transform.Position.Y)), 0f)) *
                    Microsoft.Xna.Framework.Matrix.CreateRotationZ(Transform.Rotation) *
                    Microsoft.Xna.Framework.Matrix.CreateScale(Zoom) *
                    Microsoft.Xna.Framework.Matrix.CreateTranslation(Vector3.Zero);
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
            Transform.Position = Vector3.Zero;
            Zoom = 1;
        }
    }
}
