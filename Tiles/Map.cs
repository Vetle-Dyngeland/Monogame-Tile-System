namespace Tile_System.Tiles
{
    public class Map
    {
        private readonly List<CollisionTile> collisionTiles = new();
        public List<CollisionTile> CollisionTiles {
            get { return collisionTiles; }
        }

        private int width, height;
        public int Width {
            get { return width; }
        }
        public int Height {
            get { return height; }
        }

        public void Generate(int[,] map, int size, bool drawEmpty)
        {
            collisionTiles.Clear();
            for(int x = 0; x < map.GetLength(1); x++) {
                for(int y = 0; y < map.GetLength(0); y++) {
                    int num = map[y, x];
                    collisionTiles.Add(new(num, new Rectangle(x * size, y * size, size, size)));
                    collisionTiles[^1].shouldCollide = num > 0;
                    collisionTiles[^1].shouldDraw = num > 0 || drawEmpty;
                }
            }

            width = map.GetLength(1) * size;
            height = map.GetLength(0) * size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var tile in collisionTiles) {
                tile.Draw(spriteBatch);
            }
        }
    }
}