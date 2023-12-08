namespace Pyrite.Core.Physics.Colliders
{
    public class CircleCollider : Collider
    {
        public int Radius = 1;

        public CircleCollider(int radius, Point offset)
        {
            _bounds = new Rectangle()
            {
                Location = (Parent == null ? Point.Zero : Parent.WorldTransform.Position) + offset,
                Size = new Point(radius * 2, radius * 2),
            };

            Radius = radius;
        }
    }
}
