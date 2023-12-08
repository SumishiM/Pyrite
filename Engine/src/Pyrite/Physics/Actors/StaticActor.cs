namespace Pyrite.Physics
{
    /// <summary>
    /// Class for Physic Actors that act as solid objects which cannot be moved by <see cref="DynamicActor"/> by default.
    /// This kind of actor will move no matter the other collisions from other actors.
    /// </summary>
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
            Point Position = Collider.Location;

            if (moveX != 0)
            {
                _xRemainder -= moveX;
                Position.X = moveX;
                if( moveX > 0)
                {
                    // Moving rigth
                    foreach (DynamicActor actor in ridingActors) // PhysicActors.AllDynamicActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor.Collider))
                        {
                            // push right
                            actor.MoveX(this.Collider.Right - actor.Collider.Left, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveX(moveX);
                        }
                    }
                }
                else
                {
                    // Moving left
                    foreach (DynamicActor actor in ridingActors) // PhysicActors.AllDynamicActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor.Collider))
                        {
                            // push left
                            actor.MoveX(Collider.Left - actor.Collider.Right, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry left 
                            actor.MoveX(moveX);
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
                    // Moving Top
                    foreach (DynamicActor actor in ridingActors) // PhysicActors.AllDynamicActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor.Collider))
                        {
                            // push right
                            actor.MoveY(this.Collider.Top - actor.Collider.Bottom, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveY(moveX);
                        }
                    }
                }
                else
                {
                    // Moving down
                    foreach (DynamicActor actor in ridingActors) // PhysicActors.AllDynamicActors
                    {
                        if (actor.Collider == null)
                            continue;

                        if (Collider.OverlapWith(actor.Collider))
                        {
                            // push right
                            actor.MoveY(Collider.Bottom - actor.Collider.Top, actor.Squish);
                        }
                        else if (ridingActors.Contains(actor))
                        {
                            //Carry right 
                            actor.MoveY(moveX);
                        }
                    }
                }
            }
        }
    }
}
