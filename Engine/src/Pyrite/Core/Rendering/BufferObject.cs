using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Rendering
{
    public class BufferObject <T> : IDisposable
        where T : unmanaged
    {

        //Our handle and the GL instance this class will use, these are private because they have no reason to be public.
        //Most of the time you would want to abstract items to make things like this invisible.
        private uint _handle;
        private GL _gl;

        private BufferTargetARB _bufferType;

        public unsafe BufferObject(GL gl, Span<T> data, BufferTargetARB bufferType)
        {
            _gl = gl;
            _bufferType = bufferType;

            _handle = _gl.GenBuffer();
            Bind();
            fixed(void* d = data)
            {
                _gl.BufferData(bufferType, (nuint)(data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);
            }
        }

        public void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}
