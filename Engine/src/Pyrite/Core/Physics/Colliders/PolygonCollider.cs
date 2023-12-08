namespace Pyrite.Core.Physics.Colliders
{
    public class PolygonCollider : Collider
    {
        private Point _leftestPoint;
        private Point _rightestPoint;
        private Point _highestPoint;
        private Point _lowestPoint;

        public bool IsConvex = true;
        public ICollection<Point> Points { get; protected set; }

        public PolygonCollider(ICollection<Point> points)
        {
            Points = points;
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            _leftestPoint = Points.OrderBy(p => p.X).ToList()[0];
            _rightestPoint = Points.OrderByDescending(p => p.X).ToList()[0];
            _highestPoint = Points.OrderBy(p => p.Y).ToList()[0];
            _lowestPoint = Points.OrderByDescending(p => p.Y).ToList()[0];

            // Create a bound that encapsulate the collision to make the collision faster
            _bounds = new Rectangle(Point.Zero, new Point(_leftestPoint.X - _rightestPoint.X, _lowestPoint.Y - _highestPoint.Y));
        }
    }
}
