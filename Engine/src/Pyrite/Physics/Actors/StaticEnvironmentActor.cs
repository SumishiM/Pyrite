using Pyrite.Physics.Colliders;
using System.Diagnostics.CodeAnalysis;

namespace Pyrite.Physics
{
    /// <summary>
    /// Class for environment <see cref="Colliders.Collider"/>s.
    /// The objective of this class is to allow the level to use simplified colliders set by the user.
    /// This type of <see cref="PhysicActor"/> will never move and remain at the same place for it's entire lifecycle.
    /// </summary>
    public class StaticEnvironmentActor : PhysicActor
    {
        public override Collider? Collider => Colliders.First();
        public ICollection<Collider> Colliders { get; set; }

        public StaticEnvironmentActor()
        {
            Colliders = new List<Collider>();
        }

        public StaticEnvironmentActor(ICollection<Collider> colliders)
        {
            Colliders = colliders;
            UpdateBounds();
        }

        public void AddCollider([DisallowNull] Collider collider)
        {
            if (!collider.IsStatic || Colliders.Contains(collider))
                return;

            Colliders.Add(collider);
            UpdateBounds();
        }

        public void RemoveCollider([DisallowNull] Collider collider)
        {
            if (Colliders.Contains(collider))
            {
                Colliders.Remove(collider);
                UpdateBounds();
            }
        }

        public void UpdateBounds()
        {
            Point topLeft = new Point(0, 0);
            Point size = new Point(0, 0);

            // calculate the leftest collider
            // + highest
            // righest
            // lowest
            // then recreate a bound from this informations
        }

        public void Clear()
        {
            Colliders.Clear();
        }
    }
}
