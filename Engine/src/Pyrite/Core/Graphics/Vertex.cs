using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaColor = Microsoft.Xna.Framework.Color;


namespace Pyrite.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex : IVertexType
    {
        public Vector3 Position;
        public XnaColor Color;
        public Vector2 TextureCoordinate;
        public Vector3 BlendType;

        public static readonly VertexDeclaration VertexDeclaration;

        readonly VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
    
        static Vertex()
        {
            var elements = new VertexElement[]
            {
                new(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new(16, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
                new(24, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 1),
            };

            VertexDeclaration = new VertexDeclaration(elements);
        }

        public Vertex(Vector3 position, XnaColor color, Vector2 texcoord, Vector3 blend)
        {
            Position = position;
            Color = color;
            TextureCoordinate = texcoord;
            BlendType = blend;
        }

        public override int GetHashCode()
        {
            // TODO: Fix GetHashCode
            return (Position.GetHashCode() + Color.GetHashCode() + BlendType.GetHashCode()) / 3;
        }

        public override string ToString()
        {
            return (
                "{{Position:" + Position.ToString() +
                " Color:" + Color.ToString() +
                " Blend:" + BlendType.ToString() +
                "}}"
            );
        }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Color == right.Color
                   && left.Position == right.Position
                   && left.BlendType == right.BlendType;
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != base.GetType())
            {
                return false;
            }
            return (this == ((Vertex)obj));
        }

    }
}