
namespace Pyrite.Core.Graphics
{
    public class Batch2D
    {
        public string Name;

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

    }
}