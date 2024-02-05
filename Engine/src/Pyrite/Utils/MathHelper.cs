

using Pyrite.Core.Geometry;

namespace Pyrite.Utils
{
    public static class MathHelper
    {
        #region Single

        #endregion

        #region Line
        public static float Length(this Line line)
            => (line.Start - line.End).Length();

        public static float SquaredLength(this Line line)
            => (line.Start - line.End).SquaredLength();
        #endregion
    }
}
