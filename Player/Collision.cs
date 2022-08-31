namespace Tile_System.Player
{
    public class Collision
    {
        private Main player;
        private bool groundedSet;

        public Collision(Main player)
        {
            this.player = player;
        }

        public void Update(List<CollisionTile> tiles)
        {
            groundedSet = false;
            foreach(var t in tiles) {
                if(!t.shouldCollide)
                    continue;

                Collide(t.Rect);
            }
            player.movement.grounded = groundedSet;
        }

        private void Collide(Rectangle rectangle)
        {
            Main p = player;

            if(p.rect.TouchingTop(rectangle, p.movement.velocity)) {
                p.position.Y = rectangle.Y - p.rect.Height;
                p.movement.velocity.Y = 0;
                groundedSet = true;
            }
            if(p.rect.TouchingBottom(rectangle, p.movement.velocity)) {
                p.position.Y = rectangle.Y + p.rect.Height; ;
                p.movement.velocity.Y = 0;
            }
            if(p.rect.TouchingLeft(rectangle, p.movement.velocity)) {
                p.position.X = rectangle.X - p.rect.Width;
                p.movement.velocity.X = 0;
            }
            if(p.rect.TouchingRight(rectangle, p.movement.velocity)) {
                p.position.X = rectangle.X + rectangle.Width;
                p.movement.velocity.X = 0;
            }
        }
    }
}
