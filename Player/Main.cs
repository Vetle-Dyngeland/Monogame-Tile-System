using Tile_System.Helpers;

namespace Tile_System.Player
{
    public class Main
    {
        public readonly Input input;
        public readonly Movement movement;
        public readonly Collision collision;

        private Texture2D texture;
        public Vector2 position;
        private readonly int size;

        public Rectangle rect;

        public Main(int tileSize)
        {
            size = tileSize;

            input = new();
            movement = new(this, size);
            collision = new(this);
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Sprites/testPlayer");
        }

        public void Update(GameTime gameTime, List<CollisionTile> tiles)
        {
            input.Update();
            movement.Update(gameTime);
            collision.Update(tiles);

            ResetPosition(tiles);

            rect = new(position.ToPoint(), new(size, size));
        }

        private void ResetPosition(List<CollisionTile> tiles)
        {
            float lowestY = 0;
            foreach(var tile in tiles)
                if(tile.Rect.Bottom > lowestY)
                    lowestY = tile.Rect.Bottom;

            if(position.Y > lowestY) position = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
