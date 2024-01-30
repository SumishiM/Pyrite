using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    public class VertexArrayObject<TVertex, TIndex> : IDisposable
        where TVertex : unmanaged
        where TIndex : unmanaged
    {
        //Our handle and the GL instance this class will use, these are private because they have no reason to be public.
        //Most of the time you would want to abstract items to make things like this invisible.
        private readonly uint _handle;
        private readonly GL _gl;

        public VertexArrayObject(GL gl, BufferObject<TVertex> vbo, BufferObject<TIndex> ebo)
        {
            _gl = gl;
            _handle = _gl.GenVertexArray();

            Bind();

            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
        {
            //Setting up a vertex attribute pointer
            _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertex), (void*)(offset * sizeof(TVertex)));
            _gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            //Binding the vertex array.
            _gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            //Remember to dispose this object so the data GPU side is cleared.
            //We dont delete the VBO and EBO here, as you can have one VBO stored under multiple VAO's.
            _gl.DeleteVertexArray(_handle);
            GC.SuppressFinalize(this);
        }
    }
}
