using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Core.Geometry;
using XnaVector2 = Microsoft.Xna.Framework.Vector2;
using XnaVector3 = Microsoft.Xna.Framework.Vector3;
using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
using XnaColor = Microsoft.Xna.Framework.Color;


namespace Pyrite.Core.Graphics
{
    public class BatchItem
    {
        public Texture2D? Texture;
        public Vertex[] VertexData = new Vertex[4];
        public int[] IndexData = new int[6];
        public int VertexCount = 4;

        public BatchItem() { }

        private readonly int[] _defaultIndexData = new int[6] { 3, 0, 2, 2, 0, 1 };

        public void Set(
            Texture2D texture,
            XnaVector2 position,
            XnaVector2 destinationSize,
            XnaRectangle? sourceRectangle,
            float rotation,
            XnaVector2 scale,
            ImageFlip flip,
            XnaColor color,
            XnaVector2 origin,
            XnaVector3 colorBlend,
            float layerDepth = 1f)
        {
            Texture = texture;
            VertexCount = 4;
            IndexData[0] = 3;
            IndexData[1] = 0;
            IndexData[2] = 2;
            IndexData[3] = 2;
            IndexData[4] = 0;
            IndexData[5] = 1;

            if (!sourceRectangle.HasValue)
            {
                sourceRectangle = new(0, 0, texture.Width, texture.Height);
            }

            // calculate corners
            XnaVector2 topLeft = -origin * scale;
            XnaVector2 topRight = (-origin + new XnaVector2(destinationSize.X, 0f)) * scale;
            XnaVector2 bottomRight = (-origin + new XnaVector2(0f, destinationSize.Y)) * scale;
            XnaVector2 bottomLeft = (-origin + destinationSize) * scale;

            if (rotation != 0)
            {
                // calculate radiant angles
                float cos = MathF.Cos(rotation);
                float sin = MathF.Sin(rotation);

                // rotate points 
                topLeft = new XnaVector2(topLeft.X * cos - topLeft.Y * sin, topLeft.X * sin + topLeft.Y * cos);
                topRight = new XnaVector2(topRight.X * cos - topRight.Y * sin, topRight.X * sin + topRight.Y * cos);
                bottomRight = new XnaVector2(bottomRight.X * cos - bottomRight.Y * sin, bottomRight.X * sin + bottomRight.Y * cos);
                bottomLeft = new XnaVector2(bottomLeft.X * cos - bottomLeft.Y * sin, bottomLeft.X * sin + bottomLeft.Y * cos);
            }

            VertexData[0] = new Vertex(
                new XnaVector3(position + topLeft, layerDepth),
                color,
                new XnaVector2((float)sourceRectangle.Value.Left / texture.Width, (float)sourceRectangle.Value.Top / texture.Height),
                colorBlend
            );

            VertexData[1] = new Vertex(
                new XnaVector3(position + topRight, layerDepth),
                color,
                new XnaVector2((float)sourceRectangle.Value.Right / texture.Width, (float)sourceRectangle.Value.Top / texture.Height),
                colorBlend
            );

            VertexData[2] = new Vertex(
                new XnaVector3(position + bottomRight, layerDepth),
                color,
                new XnaVector2((float)sourceRectangle.Value.Right / texture.Width, (float)sourceRectangle.Value.Bottom / texture.Height),
                colorBlend
            );

            VertexData[3] = new Vertex(
                new XnaVector3(position + bottomLeft, layerDepth),
                color,
                new XnaVector2((float)sourceRectangle.Value.Left / texture.Width, (float)sourceRectangle.Value.Bottom / texture.Height),
                colorBlend
            );

            if ((flip & ImageFlip.Horizontal) != ImageFlip.None)
            {
				(VertexData[0].TextureCoordinate, VertexData[1].TextureCoordinate) = (VertexData[1].TextureCoordinate, VertexData[0].TextureCoordinate);
				(VertexData[3].TextureCoordinate, VertexData[2].TextureCoordinate) = (VertexData[2].TextureCoordinate, VertexData[3].TextureCoordinate);
			}

			if ((flip & ImageFlip.Vertical) != ImageFlip.None)
            {
				(VertexData[1].TextureCoordinate, VertexData[2].TextureCoordinate) = (VertexData[2].TextureCoordinate, VertexData[1].TextureCoordinate);
				(VertexData[0].TextureCoordinate, VertexData[3].TextureCoordinate) = (VertexData[3].TextureCoordinate, VertexData[0].TextureCoordinate);
			}
		}
    }
}