namespace Tile_System.Tiles
{
    public class Tile
    {
        public Texture2D texture;
        public Color color = Color.White;
        public bool shouldDraw = true;

        private Rectangle rect;
        public Rectangle Rect {
            get { return rect; }
            protected set { rect = value; }
        }

        private static ContentManager content;
        public static ContentManager Content {
            protected get { return content; }
            set { content = value; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(shouldDraw)
                spriteBatch.Draw(texture, rect, color);
        }
    }

    public class CollisionTile : Tile
    {
        public bool shouldCollide = true;

        public CollisionTile(int i, Rectangle newRect)
        {
            texture = Content.Load<Texture2D>("Sprites/Tiles/Tile" + i);
            Rect = newRect;
        }
    }
}
