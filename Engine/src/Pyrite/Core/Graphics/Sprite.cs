﻿using Pyrite.Core.Graphics.Shaders;
using Pyrite.Utils;
using System.Numerics;

namespace Pyrite.Core.Graphics
{
    public class Sprite : IDisposable
    {

        public readonly Shader Shader;
        public readonly Texture Texture;
        public readonly Transform Transform;

        public int SortingOrder { get; set; }

        public Matrix4x4 ModelMatrix =>
            Matrix4x4.CreateScale(Texture.Size.X * Transform.Scale.X, Texture.Size.Y * Transform.Scale.Y, 1f) *
            Matrix4x4.CreateRotationZ(Transform.Rotation.ToRadians()) *
            Matrix4x4.CreateTranslation(Transform.Position.X, Transform.Position.Y, 0f);

        public Sprite ( string path, Transform transform )
        {
            Shader = Shader.Default;
            Texture = new(path);
            Transform = transform;
        }

        public void Dispose ()
        {
            Shader.Dispose();
            Texture.Dispose();

            GC.SuppressFinalize( this );
        }
    }
}
