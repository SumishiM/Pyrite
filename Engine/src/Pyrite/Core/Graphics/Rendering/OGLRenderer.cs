using Silk.NET.Input;
using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    public class OGLRenderer : Renderer
    {
        private GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<float> Vbo;
        private BufferObject<uint> Ebo;
        private VertexArrayObject<float, uint> Vao;

        private Shaders.Shader Shader;
        public OGLTexture Texture;

        //Vertex data, uploaded to the VBO.
        private static readonly float[] Vertices =
        {
            //X    Y      Z     S    T
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f,
             0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,
            -0.5f,  0.5f, 0.5f, 0.0f, 0.0f
        };

        //Index data, uploaded to the EBO.
        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public unsafe override void Initialize()
        {
            Gl = Graphics.Gl;

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            // need to move
            Shader = new Shaders.Shader("Shaders\\shader.vert", "Shaders\\shader.frag");

            Texture = new OGLTexture("Content\\silk.png");
        }

        public unsafe override void Draw()
        {
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            //Binding and using our VAO and shader.
            Vao.Bind();
            Shader.Use();

            Texture.Bind(TextureUnit.Texture0);

            //Setting a uniform.
            Shader.SetUniform("uTexture", 0);

            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
        }

        public override void Dispose()
        {
            //Remember to dispose all the instances.
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
            Texture.Dispose();
        }
    }
}
