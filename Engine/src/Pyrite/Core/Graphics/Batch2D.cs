
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Assets;
using XnaColor = Microsoft.Xna.Framework.Color;

namespace Pyrite.Core.Graphics
{
    public class Batch2D
    {
        public string? Name;

        public const int START_BATCH_ITEM_COUNT = 128;

        private Vertex[] _vertices = new Vertex[START_BATCH_ITEM_COUNT * 4];
        private int[] _indices = new int[START_BATCH_ITEM_COUNT * 4];

        private Vertex[] _vertexBuffer = new Vertex[START_BATCH_ITEM_COUNT * 4];
        private short[] _indexBuffer = new short[START_BATCH_ITEM_COUNT * 6];

        private BatchItem[] _batchItems = new BatchItem[START_BATCH_ITEM_COUNT];
        private BatchItem[]? _transparencyBatchItems;

        public int TotalItemCount => _batchItems.Length;
        public int TotalTransparencyItemCount => _transparencyBatchItems?.Length ?? 0;

        private int _nextItemIndex;
        private int _nextTransparencyItemIndex;

        public bool IsBatching = false;
        
        //public GraphicsDevice GraphicsDevice { get; set; }
        //public readonly BatchMode BatchMode;
        //public readonly BlendState BlendState;
        //public readonly SamplerState SamplerState;
        //public readonly DepthStencilState DepthStencilState;
        //public readonly RasterizerState RasterizerState;

        
        public void Draw(
            AssetReference<TextureAsset> asset,
            Vector2 position,
            Vector2 targetSize,
            Rectangle sourceRectangle,
            float sort,
            float rotation,
            Vector2 scale,
            ImageFlip flip,
            XnaColor color,
            Vector2 offset,
            Vector3 blendStyle)
        {
            if (asset.TryAsset is not TextureAsset texAsset)
                return;

            //ref BatchItem item = ref GetBatchItem(color.A < byte.MaxValue);
            //item.Set(texAsset.Texture, position, targetSize, sourceRectangle, rotation, scale, flip, color, offset, blendStyle, sort);
        }


        //private ref BatchItem GetBatchItem(bool needsTransparency)
        //{
        //
        //}
    }
}