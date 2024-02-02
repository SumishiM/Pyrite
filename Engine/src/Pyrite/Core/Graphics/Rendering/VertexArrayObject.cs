using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    public class VertexArrayObject<TVertex, TIndex> : IDisposable
        where TVertex : unmanaged
        where TIndex : unmanaged
    {
        private readonly uint _handle;

        public VertexArrayObject(BufferObject<TVertex> vbo, BufferObject<TIndex> ebo)
        {
            _handle = Graphics.Gl.GenVertexArray();

            Bind();

            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
        {
            //Setting up a vertex attribute pointer
            Graphics.Gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertex), (void*)(offset * sizeof(TVertex)));
            Graphics.Gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            //Binding the vertex array.
            Graphics.Gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            //Remember to dispose this object so the data GPU side is cleared.
            //We dont delete the VBO and EBO here, as you can have one VBO stored under multiple VAO's.
            Graphics.Gl.DeleteVertexArray(_handle);
            GC.SuppressFinalize(this);
        }
    }
}
