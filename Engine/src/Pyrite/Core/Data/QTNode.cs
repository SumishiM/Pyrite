using Pyrite.Graphics;
using System.Collections.Immutable;

namespace Pyrite.Core.Data
{
    public readonly struct QTNodeInfo<T>(T entity, Rectangle bounds) where T : notnull
    {
        public readonly T Entity = entity;
        public readonly Rectangle Bounds = bounds;
    }

    public class QTNode<T> : IDebugDrawable where T : notnull
    {
        private const int MAX_OBJECTS = 6;
        private const int MAX_LEVELS = 6;

        public ImmutableArray<QTNode<T>> Children = ImmutableArray.Create<QTNode<T>>();
        public readonly Rectangle Bounds;
        public readonly int Level = 0;

        public readonly Dictionary<uint, QTNodeInfo<T>> Entities = new(MAX_OBJECTS);

        public QTNode(int level, Rectangle bounds)
        {
            Bounds = bounds;
            Level = level;
        }

        public void DebugDraw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recursively clears all entities of the node, but keeps the structure
        /// </summary>
        public void Clear()
        {
            Entities.Clear();
            Children = ImmutableArray.Create<QTNode<T>>();
            if (!Children.IsDefaultOrEmpty)
            {
                for (int i = 0; i < Children.Length; i++)
                {
                    Children[i].Clear();
                }
            }
        }

        /// <summary>
        /// Completely resets the node removing anything inside
        /// </summary>
        public void Reset()
        {
            Entities.Clear();
            Children = ImmutableArray.Create<QTNode<T>>();
        }

        public void Split()
        {
            var builder = ImmutableArray.CreateBuilder<QTNode<T>>(4);
            int subWidth = Bounds.Width / 2;
            int subHeight = Bounds.Height / 2;
            int x = Bounds.X;
            int y = Bounds.Y;

            builder.Add(new QTNode<T>(Level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight)));
            builder.Add(new QTNode<T>(Level + 1, new Rectangle(x, y, subWidth, subHeight)));
            builder.Add(new QTNode<T>(Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight)));
            builder.Add(new QTNode<T>(Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight)));

            Children = builder.ToImmutableArray();
        }

        /// <summary>
        /// Determine which node the object belongs to. -1 means
        /// object cannot completely fit within a child node and is part
        /// of the parent node
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        private int GetIndex(Rectangle boundingBox)
        {
            int index = -1;
            float verticalMidpoint = Bounds.X + (Bounds.Width / 2f);
            float horizontalMidpoint = Bounds.Y + (Bounds.Height / 2f);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (boundingBox.Top < horizontalMidpoint && boundingBox.Bottom < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (boundingBox.Top > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (boundingBox.Left < verticalMidpoint && boundingBox.Right < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (boundingBox.Left > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /// <summary>
        /// Removes an entity from the quadtree
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public bool Remove(uint entityId)
        {
            bool success = false;
            if (Entities.Remove(entityId))
            {
                success = true;
            }

            if (!Children.IsDefaultOrEmpty)
            {
                for (int i = 0; i < Children.Length; i++)
                {
                    if (Children[i].Remove(entityId))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Insert the object into the quadtree. If the node
        /// exceeds the capacity, it will split and add all
        /// objects to their corresponding nodes.
        /// </summary>
        public void Insert(uint entityId, T info, Rectangle boundingBox)
        {
            if (!Children.IsDefaultOrEmpty)
            {
                int index = GetIndex(boundingBox);

                if (index != -1)
                {
                    Children[index].Insert(entityId, info, boundingBox);

                    return;
                }
            }

            Entities[entityId] = new QTNodeInfo<T>(info, boundingBox);

            if (Entities.Count > MAX_OBJECTS && Level < MAX_LEVELS)
            {
                if (Children.IsDefaultOrEmpty)
                {
                    Split();
                }

                foreach (var e in Entities)
                {
                    var bb = e.Value.Bounds;
                    int index = GetIndex(bb);
                    if (index != -1)
                    {
                        Children[index].Insert(e.Key, e.Value.Entity, bb);
                        Entities.Remove(e.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Insert the object into the quadtree from a <paramref name="point"/> position.
        /// </summary>
        public void Insert(uint entityId, T info, Point point)
        {
            Insert(entityId, info, new Rectangle(point, new Point(1, 1)));
        }

        /// <summary>
        /// Insert the object into the quadtree from it's <paramref name="transform"/>.
        /// </summary>
        public void Insert(uint entityId, T info, Transform transform)
        {
            Insert(entityId, info, new Rectangle(transform.Position, new Point(1, 1)));
        }

        /// <summary>
        /// Return all objects that could collide with the given object at <paramref name="returnEntities"/>.
        /// </summary>
        public void Retrieve(Rectangle bounds, List<QTNodeInfo<T>> returnEntities)
        {
            if (!Children.IsDefaultOrEmpty)
            {
                int index = GetIndex(bounds);
                if (index != -1) // This bounding box can be contained inside a single node
                {
                    Children[index].Retrieve(bounds, returnEntities);
                }
                else
                {
                    for (int i = 0; i <= 3; i++) // The Bounding box is to big to be contained by a single node
                    {
                        Children[i].Retrieve(bounds, returnEntities);
                    }
                }
            }

            foreach (var item in Entities)
            {
                returnEntities.Add(item.Value);
            }
        }
    }
}

