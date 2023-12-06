

using Microsoft.Xna.Framework;
using System.Net.Security;

namespace Pyrite.Physics
{
    public class StaticActor : PhysicActor
    {
        // https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
        public void Move(float x, float y)
        {

            if (this.Collider == null) return;

            _xRemainder += x;
            _yRemainder += y;

            int moveX = (int)MathF.Round(_xRemainder);
            int moveY = (int)MathF.Round(_yRemainder);

            if (moveX == 0 && moveY == 0)
                return; // No movement to apply

            IList<DynamicActor> ridingActors = new List<DynamicActor>();
            Vector2 Position = Parent.Transform.Position;

            if (moveX != 0)
            {
                _xRemainder -= moveX;
                Position.X = moveX;
                if( moveX > 0)
                {
                    foreach (DynamicActor actor in ridingActors) // Level.AllActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor))
                        {
                            // push right
                            actor.MoveX(this.Collider.Right - actor.Collider.Left, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveX(moveX, null);
                        }
                    }
                }
                else
                {
                    foreach (DynamicActor actor in ridingActors) // Level.AllActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor))
                        {
                            // push right
                            actor.MoveX(Collider.Left - actor.Collider.Right, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveX(moveX, null);
                        }
                    }
                }
            }

            if (moveY != 0)
            {
                _yRemainder -= moveY;
                Position.Y = moveY;
                if (moveY > 0)
                {
                    foreach (DynamicActor actor in ridingActors) // Level.AllActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor))
                        {
                            // push right
                            actor.MoveY(this.Collider.Top - actor.Collider.Bottom, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveY(moveX, null);
                        }
                    }
                }
                else
                {
                    foreach (DynamicActor actor in ridingActors) // Level.AllActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor))
                        {
                            // push right
                            actor.MoveY(Collider.Bottom - actor.Collider.Top, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveY(moveX, null);
                        }
                    }
                }
            }
        }
    }
}
