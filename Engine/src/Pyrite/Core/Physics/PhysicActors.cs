using Pyrite.Core.Data;

namespace Pyrite.Core.Physics
{
    public class PhysicActors
    {
        public static ICollection<DynamicActor> DynamicActors { get; private set; } = [];
        public static ICollection<StaticActor> StaticActors { get; private set; } = [];
        public static ICollection<StaticEnvironmentActor> StaticEnvironmentActors { get; private set; } = [];

        public static IEnumerable<PhysicActor> AllStaticActors =>
            StaticActors.Concat<PhysicActor>(StaticEnvironmentActors);

        public static IEnumerable<PhysicActor> AllActors => 
            DynamicActors
                .Concat<PhysicActor>(StaticActors)
                .Concat(StaticEnvironmentActors);

        public static QuadTree<ObjectNode> QuadTree { get; private set; }
    }
}
