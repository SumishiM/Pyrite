using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    public class BufferObject <T> : IDisposable
        where T : unmanaged
    {
        private readonly uint _handle;
        private readonly BufferTargetARB _bufferType;

        public unsafe BufferObject(Span<T> data, BufferTargetARB bufferType)
        {
            _bufferType = bufferType;

            _handle = Graphics.Gl.GenBuffer();
            Bind();
            fixed(void* d = data)
            {
                Graphics.Gl.BufferData(bufferType, (nuint)(data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);
            }
        }

        public void Bind()
        {
            Graphics.Gl.BindBuffer(_bufferType, _handle);
        }

        public void Dispose()
        {
            Graphics.Gl.DeleteBuffer(_handle);
        }
    }
}
