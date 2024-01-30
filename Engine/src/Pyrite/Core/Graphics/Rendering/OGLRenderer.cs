using Silk.NET.OpenGL;
using Silk.NET.Vulkan;
using System.Drawing;
using System.Numerics;

namespace Pyrite.Core.Graphics.Rendering
{
    public class OGLRenderer : RendererBase
    {
#nullable disable
        private readonly GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<float> _vbo; //! might move to renderer
        private BufferObject<uint> _ebo; //! might move to renderer
        private VertexArrayObject<float, uint> _vao; //! might move to renderer
#nullable enable


        //Vertex data, uploaded to the VBO.
        private static readonly float[] _vertices = //! might move to renderer
        [
            //X    Y      Z     S    T
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f,
             0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,
            -0.5f,  0.5f, 0.0f, 0.0f, 0.0f
        ];

        //Index data, uploaded to the EBO.
        private static readonly uint[] _indices = //! might move to renderer
        [
            0, 1, 3,
            1, 2, 3
        ];

        /// <inheritdoc/>
        public unsafe override void Initialize()
        {
            //Instantiating our new abstractions
            _ebo = new BufferObject<uint>(Graphics.Gl, _indices, BufferTargetARB.ElementArrayBuffer);
            _vbo = new BufferObject<float>(Graphics.Gl, _vertices, BufferTargetARB.ArrayBuffer);
            _vao = new VertexArrayObject<float, uint>(Graphics.Gl, _vbo, _ebo);

            //Telling the VAO object how to lay out the attribute pointers
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
        }

        /// <summary>
        /// Draw every sprites registered, note that this function do 1 draw call per sprite.
        /// </summary>
        /// <exception cref="ArgumentNullException">Main <see cref="Camera"/> is null</exception>
        public unsafe override void Draw()
        {
            if ( Camera.Main == null )
            {
                throw new ArgumentNullException(
                    nameof(Camera.Main), 
                    "No main camera found for render.");
            }

            // Clear screen
            Graphics.Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            Graphics.Gl.ClearColor(Window.BackgroundColor);

            // Set default shader and bind vao
            Shaders.Shader shader = Shaders.Shader.Default;
            _vao.Bind();

            // draw every sprites
            foreach ( var sprite in _sprites.OrderBy(s => s.SortingOrder))
            {
                shader = sprite.Shader ?? Shaders.Shader.Default;
                shader.Use();
                sprite.Texture.Bind();
                shader.SetUniform("uTexture0", 0);
                shader.SetUniform("uModel", sprite.ModelMatrix);
                shader.SetUniform("uProjection", Camera.Main.ProjectionMatrix);

                Graphics.Gl.DrawElements(
                    PrimitiveType.Triangles,
                    (uint)_indices.Length,
                    DrawElementsType.UnsignedInt,
                    null);
            }
        }

        public override void Dispose()
        {
            //Remember to dispose all the instances.
            _vbo.Dispose();
            _ebo.Dispose();
            _vao.Dispose();
            _sprites.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
