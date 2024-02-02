using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;

namespace Pyrite.Core.Graphics.Rendering
{
    public class OGLTexture : IDisposable
    {
        private readonly uint _handle;
        public Vector2 Size { get; private set; }

        public unsafe OGLTexture(string path)
        {
            //Generating the opengl handle;
            _handle = Graphics.Gl.GenTexture();
            Bind();

            using (var img = Image.Load<Rgba32>(path))
            {
                Size = new Vector2(img.Bounds.Width, img.Bounds.Height);
                Graphics.Gl.TexImage2D(
                    TextureTarget.Texture2D, 
                    0, 
                    InternalFormat.Rgba8, 
                    (uint)img.Width, 
                    (uint)img.Height, 
                    0, 
                    PixelFormat.Rgba, 
                    PixelType.UnsignedByte, 
                    null);

                img.ProcessPixelRows(accessor =>
                {
                    for (int y = 0; y < accessor.Height; y++)
                    {
                        fixed (void* data = accessor.GetRowSpan(y))
                        {
                            Graphics.Gl.TexSubImage2D(
                                TextureTarget.Texture2D, 
                                0, 
                                0, 
                                y, 
                                (uint)accessor.Width, 
                                1, 
                                PixelFormat.Rgba,
                                PixelType.UnsignedByte,
                                data);
                        }
                    }
                });
            }

            SetParameters();
        }

        public unsafe OGLTexture(Span<byte> data, uint width, uint height)
        {
            //Saving the gl instance.

            //Generating the opengl handle;
            _handle = Graphics.Gl.GenTexture();
            Bind();

            //We want the ability to create a texture using data generated from code aswell.
            fixed (void* d = &data[0])
            {
                //Setting the data of a texture.
                Graphics.Gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
                SetParameters();
            }
        }

        private void SetParameters()
        {
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
            Graphics.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
            Graphics.Gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
        {
            //When we bind a texture we can choose which textureslot we can bind it to.
            Graphics.Gl.ActiveTexture(textureSlot);
            Graphics.Gl.BindTexture(TextureTarget.Texture2D, _handle);
        }

        public void Dispose()
        {
            //In order to dispose we need to delete the opengl handle for the texure.
            Graphics.Gl.DeleteTexture(_handle);
        }
    }
}
