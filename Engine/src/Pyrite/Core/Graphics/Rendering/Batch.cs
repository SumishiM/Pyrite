using Silk.NET.OpenGL;
using Silk.NET.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Graphics.Rendering
{
    //https://github.com/Dobbermann2/Framework2D/blob/main/Framework2D/Graphics/Batch.cs
    internal class Batch
    {
        const int MAX_TEXTURES = 10;
        const int MAX_QUADS = 10000;
        const int MAX_VERTICES = MAX_QUADS * 4;
        const int MAX_INDICES = MAX_QUADS * 6;


        private Vertex[] _vertices = new Vertex[MAX_VERTICES];
        public Vertex[] Vertices => _vertices;

        private int[] _indices = new int[MAX_INDICES];
        public int[] indices => _indices;

        public int Quads = 0;

#nullable disable
        //Our new abstracted objects, here we specify what the types are.
        private BufferObject<Vertex> _vbo; //! might move to renderer
        private BufferObject<int> _ebo; //! might move to renderer
        private VertexArrayObject<Vertex, int> _vao; //! might move to renderer
#nullable enable

        private List<TextureSlot> _items;

        public Batch()
        {
            _items = [];

            _ebo = new BufferObject<int>(_indices, BufferTargetARB.ElementArrayBuffer);
            _vbo = new BufferObject<Vertex>(_vertices, BufferTargetARB.ArrayBuffer);
            _vao = new VertexArrayObject<Vertex, int>(_vbo, _ebo);

            //Telling the VAO object how to lay out the attribute pointers
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
        }

        public void Bind()
        {
            int count = 0;

            foreach (var texture in _items)
            {
                
            }
        }
        

        public void Draw()
        {
        }
    }

    public struct TextureSlot(int id)
    {
        public int id = id;
        public List<BatchItem> items = [];
    }
}
