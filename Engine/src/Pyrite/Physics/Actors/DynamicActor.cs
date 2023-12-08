using Pyrite.Diagnostics;
using Pyrite.Physics.Colliders;

namespace Pyrite.Physics
{
    /// <summary>
    /// Actors for moving object controled by player or AI.
    /// <para>Cannot collide with other <see cref="DynamicActor"/>.
    /// Also it can't be inside of a <see cref="StaticActor"/> 
    /// nor <see cref="StaticEnvironmentActor"/>.</para>
    /// </summary>
    public class DynamicActor : PhysicActor
    {
        public Vector2 Velocity { get; set; }

        public event Action? OnCollide;

        // https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
        public void MoveX(float amount, Action? SpecificOnCollide = null)
        {
            if (Parent == null)
                return;

            _xRemainder += amount;
            int move = (int)MathF.Round(_xRemainder);

            if (move == 0)
                return; // No need to move 

            _xRemainder -= move;
            int sign = MathF.Sign(move);

            while (move != 0)
            {
                if (!Collision.Check(PhysicActors.StaticActors, this, new Point(sign, 0)))
                {
                    // No static actor immediately beside us
                    // we can move the actor one pixel further
                    Parent.Transform.Position += new Point(sign, 0);
                    move -= sign;
                }
                else
                {
                    // Collide with a static actor
                    // try catch to not block the game physics
                    try
                    {
                        SpecificOnCollide?.Invoke();
                        OnCollide?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e.Message, nameof(DynamicActor.MoveX), 53);
                    }
                    break;
                }
            }
        }

        // https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
        public void MoveY(float amount, Action? SpecificOnCollide = null)
        {
            if (Parent == null)
                return;

            _yRemainder += amount;
            int move = (int)MathF.Round(_yRemainder);

            if (move == 0)
                return; // No need to move 

            _yRemainder -= move;
            int sign = MathF.Sign(move);

            while (move != 0)
            {
                if (!Collision.Check(PhysicActors.StaticActors, this, new Point(0, sign)))
                {
                    // No static actor immediately beside us
                    // we can move the actor one pixel further
                    Parent.Transform.Position += new Point(0, sign);
                    move -= sign;
                }
                else
                {
                    // Collide with a static actor
                    // try catch to not block the game physics
                    try
                    {
                        SpecificOnCollide?.Invoke();
                        OnCollide?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e.Message, nameof(DynamicActor.MoveY), 95);
                    }
                    break;
                }
            }
        }


        public override void Update()
        {
            MoveX(Velocity.X);
            MoveY(Velocity.Y);
        }


        public virtual bool IsRiding(StaticActor actor) => false;
        public virtual void Squish() { }
    }
}
