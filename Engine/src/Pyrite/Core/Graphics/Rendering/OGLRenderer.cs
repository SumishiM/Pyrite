using Silk.NET.OpenGL;
using System.Numerics;

namespace Pyrite.Core.Graphics.Rendering
{
    public class OGLRenderer : Renderer
    {
#nullable disable
        private GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<float> Vbo;
        private BufferObject<uint> Ebo;
        private VertexArrayObject<float, uint> Vao;
#nullable enable

        private Shaders.Shader Shader;
        public OGLTexture Texture;
        //Creating transforms for the transformations

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

            Texture = new OGLTexture("Content\\PyriteIcon512.png");
            //Unlike in the transformation, because of our abstraction, order doesn't matter here.
            //Translation.
            Transforms[0] = new Transform();
            Transforms[0].Position = new Vector3(0.5f, 0.5f, 0f);
            //Rotation.
            Transforms[1] = new Transform();
            Transforms[1].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
            //Scaling.
            Transforms[2] = new Transform();
            Transforms[2].Scale = 0.5f;
            //Mixed transformation.
            Transforms[3] = new Transform();
            Transforms[3].Position = new Vector3(-0.5f, 0.5f, 0f);
            Transforms[3].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
            Transforms[3].Scale = 0.5f;
        }

        public unsafe override void Draw()
        {
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            //Binding and using our VAO and shader.
            Vao.Bind();
            Shader.Use();

            Shader.SetUniform("uTexture0", 0);

            for (int i = 0; i < Transforms.Length; i++)
            {
                //Using the transformations.
                Shader.SetUniform("uModel", Transforms[i].ViewMatrix);

                Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
            }
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
