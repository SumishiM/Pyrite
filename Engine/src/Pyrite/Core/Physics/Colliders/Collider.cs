using Pyrite.Core;

namespace Pyrite.Core.Physics.Colliders
{
    /// <summary>
    /// Base collider class 
    /// </summary>
    public abstract class Collider : Component
    {
        public bool Collide { get; set; } = true;
        public bool IsStatic { get; set; } = false;

        public virtual int Left => Bounds.Location.X;
        public virtual int Right => Bounds.Location.X + Bounds.Size.X;
        public virtual int Top => Bounds.Location.Y;
        public virtual int Bottom => Bounds.Location.Y + Bounds.Size.Y;
        public Point Location => _bounds.Location;
        public virtual Vector2 HalfExtends => Bounds.Size.ToVector2() / 2f;
        public virtual Vector2 Center => Bounds.Location.ToVector2() + HalfExtends;

        public Point Offset { get; set; } = Point.Zero;

        protected Rectangle _bounds = Rectangle.Empty;
        public Rectangle Bounds => _bounds;

        public Collider()
        {
        }

        public Collider(Collider reference)
        {
            _bounds = reference.Bounds;
            Offset = reference.Offset;
        }

        public override void Update()
        {
            if( Parent != null )
            {
                _bounds.Location = Parent.WorldTransform.Position + Offset;
            }
        }

        /// <summary>
        /// Move a clone of the collider
        /// </summary>
        /// <param name="delta">Amount to move the collider</param>
        /// <returns>a copy of the collider moved of delta</returns>
        public Collider PredicateMove(Point delta)
        {
            Collider copy = (Collider)Clone();
            copy._bounds.Location += delta;
            return copy;
        }


        // todo : adapt to multiple shapes
        public bool OverlapWith(Collider other)
        {
            return Collision.ColliderToCollider(this, other);
        }
    }
}
