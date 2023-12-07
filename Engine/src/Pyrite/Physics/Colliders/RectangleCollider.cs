namespace Pyrite.Physics.Colliders
{
    public class RectangleCollider : Collider
    {
        public RectangleCollider(Point size, Point offset)
        {
            _bounds = new Rectangle
            {
                Location = (Parent == null ? Point.Zero : Parent.WorldTransform.Position) + offset,
                Size = size
            };

            Offset = offset;
        }
    }
}
