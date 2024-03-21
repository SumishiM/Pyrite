using Ignite;
using Pyrite.Components;
using Silk.NET.OpenGL;
using System.Diagnostics.CodeAnalysis;
using Shader = Pyrite.Core.Graphics.Rendering.OpenGL.Shader;

namespace Pyrite.Core.Graphics.Rendering.OpenGL
{
    public class OGLRenderer : RendererBase
    {
#nullable disable
        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<float> _vbo; //! might move to renderer
        private BufferObject<uint> _ebo; //! might move to renderer
        private VertexArrayObject<float, uint> _vao; //! might move to renderer
#nullable enable


        //Vertex data, uploaded to the VBO.
        private static readonly float[] _vertices = //! might move to renderer
        [
            //X    Y      Z     S    T
            0.5f,
            0.5f,
            0.0f,
            1.0f,
            0.0f,
            0.5f,
            -0.5f,
            0.0f,
            1.0f,
            1.0f,
            -0.5f,
            -0.5f,
            0.0f,
            0.0f,
            1.0f,
            -0.5f,
            0.5f,
            0.0f,
            0.0f,
            0.0f
        ];

        //Index data, uploaded to the EBO.
        private static readonly uint[] _indices = //! might move to renderer
        [
            0,
            1,
            3,
            1,
            2,
            3
        ];

        /// <inheritdoc/>
        public unsafe override void Initialize()
        {
            //Instantiating our new abstractions
            _ebo = new BufferObject<uint>(_indices, BufferTargetARB.ElementArrayBuffer);
            _vbo = new BufferObject<float>(_vertices, BufferTargetARB.ArrayBuffer);
            _vao = new VertexArrayObject<float, uint>(_vbo, _ebo);

            //Telling the VAO object how to lay out the attribute pointers
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
        }

        public override void ClearScreen()
        {
            // Clear screen
            Graphics.Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            Graphics.Gl.ClearColor(Window.BackgroundColor);
        }

        /// <summary>
        /// Draw every sprites registered, note that this function do 1 draw call per sprite.
        /// </summary>
        public unsafe override void Draw([DisallowNull] Transform transform, [DisallowNull]Texture texture, Shader? shader)
        {
            // Set default shader and bind vao
            shader ??= Shader.Default;
            _vao.Bind(); // vertex array object
            shader.Use();

            // send data to shader
            texture.Bind();
            shader.SetUniform("uTexture0", 0);
            shader.SetUniform("uModel", texture.GetModelMatrix(transform));
            shader.SetUniform("uProjection", Camera.Main.WorldViewProjection);

            // draw sprite
            Graphics.Gl.DrawElements(
                PrimitiveType.Triangles,
                (uint)_indices.Length,
                DrawElementsType.UnsignedInt,
                null);
        }

        public override void Dispose()
        {
            //Remember to dispose all the instances.
            _vbo.Dispose();
            _ebo.Dispose();
            _vao.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
