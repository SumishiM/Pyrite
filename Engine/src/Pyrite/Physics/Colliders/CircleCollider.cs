namespace Pyrite.Physics.Colliders
{
    public class CircleCollider : Collider
    {
        public int Radius = 1;

        public CircleCollider(Point center, int radius)
        {
            Bounds = new Rectangle()
            {
                Location = Point.Zero,
                Size = new Point(radius * 2, radius * 2),
            };
            Radius = radius;
        }
    }
}
