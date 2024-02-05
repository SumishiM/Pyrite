namespace Pyrite.Core.Geometry.Shapes
{
    public interface IShape
    {
        public Rectangle ToRectangle();
        public Circle ToInnerCircle();
        public Circle ToOuterCircle();
        //public Polygon ToPolygon();
    }
}
