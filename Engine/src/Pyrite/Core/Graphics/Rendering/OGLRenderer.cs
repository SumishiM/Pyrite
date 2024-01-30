using Silk.NET.OpenGL;
using Silk.NET.Vulkan;
using System.Drawing;
using System.Numerics;

namespace Pyrite.Core.Graphics.Rendering
{
    public class OGLRenderer : Renderer
    {
#nullable disable
        private readonly GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<float> _vbo;
        private BufferObject<uint> _ebo;
        private VertexArrayObject<float, uint> _vao;
#nullable enable


        //Vertex data, uploaded to the VBO.
        private static readonly float[] _vertices =
        [
            //X    Y      Z     S    T
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f,
             0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,
            -0.5f,  0.5f, 0.0f, 0.0f, 0.0f
        ];

        //Index data, uploaded to the EBO.
        private static readonly uint[] _indices =
        [
            0, 1, 3,
            1, 2, 3
        ];

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


        public unsafe override void Draw()
        {
            if ( Camera.Main == null )
            {
                throw new ArgumentNullException(
                    nameof(Camera.Main), 
                    "No main camera found for render.");
            }

            Graphics.Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            Graphics.Gl.ClearColor(Window.BackgroundColor);

            _vao.Bind();

            foreach ( var sprite in _sprites.OrderBy(s => s.SortingOrder))
            {
#if DEBUG
                //Console.WriteLine($"Render sprite at {sprite.Transform.Position}");
#endif
                sprite.Shader.Use();
                sprite.Texture.Bind();
                sprite.Shader.SetUniform("uTexture0", 0);
                sprite.Shader.SetUniform("uModel", sprite.ModelMatrix);
                sprite.Shader.SetUniform("uProjection", Camera.Main.ProjectionMatrix);

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
