using Microsoft.Xna.Framework;

namespace Pyrite.Physics
{
    public class DynamicActor : PhysicActor
    {
        // https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
        public void MoveX(float amount, Action? OnCollide)
        {
            _xRemainder += amount;
            int move = (int)MathF.Round(_xRemainder);

            if (move == 0) 
                return; // No need to move 

            _xRemainder -= move;
            int sign = MathF.Sign(move);

            while (move != 0)
            {
                if (!Collider(PhysicActors.StaticActor, Parent, new Vector2(sign, 0)))
                {
                    // No static actor immediately beside us
                    // we can move the actor one pixel further
                    Parent!.Transform!.Position += new Vector2(sign, 0);
                    move -= sign;
                }
                else
                {
                    // Collide with a static actor
                    OnCollide?.Invoke();
                    break;
                }
            }
        }

        // https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
        public void MoveY(float amount, Action? OnCollide)
        {
            _yRemainder += amount;
            int move = (int)MathF.Round(_yRemainder);

            if (move == 0) 
                return; // No need to move 

            _yRemainder -= move;
            int sign = MathF.Sign(move);

            while (move != 0)
            {
                if (!ColliderAt(PhysicActors.StaticActors, Parent, new Vector2(0, sign)))
                {
                    Parent!.Transform!.Position += new Vector2(0, sign);
                    move -= sign;
                }
                else
                {
                    OnCollide?.Invoke();
                    break;
                }
            }
        }

        public virtual bool IsRiding(StaticActor actor) => false;
        public virtual void Squish() { }
    }
}
