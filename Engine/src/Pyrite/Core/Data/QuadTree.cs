using Pyrite.Core.Physics;

namespace Pyrite.Core.Data
{
    public class QuadTree<T> where T : ObjectNode
    {
        public readonly QTNode<T> Root;

        public QuadTree(Point size)
        {
            Root = new QTNode<T>(0, new Rectangle(Point.Zero, size));
        }

        public QuadTree(Rectangle bounds)
        {
            Root = new QTNode<T>(0, bounds);
        }

        public void Add(T node)
        {
            if (!node.IsActive)
                return;

            Transform transform = node.WorldTransform;
            if( node.GetComponent<PhysicActor>() is PhysicActor actor ) 
            {
                if( actor.IsActive )
                {
                    Root.Insert(node.UID, node, actor.Collider!.Bounds);
                }
            }
            else
            {
                Root.Insert(node.UID, node, transform);
            }
        }

        public void Add(IEnumerable<T> nodes)
        {
            foreach (T node in nodes)
            {
                Add(node);
            }
        }

        public void Remove(T node)
        {
            Root.Remove(node.UID);
        }

        public void Remove(IEnumerable<T> nodes)
        {
            foreach(T node in nodes)
            {
                Remove(node);
            }
        }
    }
}
