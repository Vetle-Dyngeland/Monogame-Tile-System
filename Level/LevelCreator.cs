namespace Tile_System.Level
{
    public class LevelCreator
    {
        public enum Brush
        {
            Eraser,
            Grass,
            Dirt
        }
        public Brush brush = Brush.Grass;
        private Brush prevBrush;
        private Brush prevUseBrush = Brush.Grass;

        private readonly Camera camera;
        private readonly int tileSize;
        private readonly ContentManager content;

        public Vector2 mouseWorldPos;
        private Texture2D texture;
        private readonly Vector2 screenSize;

        public List<List<int>> returnCode = null;

        private readonly ICondition useEraser = new KeyboardCondition(Keys.LeftShift);
        private readonly ICondition cycleBrush = new KeyboardCondition(Keys.C);

        public bool canEditLevel;

        public LevelCreator(Camera camera, int tileSize, ContentManager content, Vector2 screenSize)
        {
            this.camera = camera;
            this.tileSize = tileSize;
            this.content = content;
            this.screenSize = screenSize;
        }

        public void Update(List<List<int>> levelCode)
        {
            returnCode = levelCode;

            if(!canEditLevel) return;

            Inputs();

            if(brush != prevBrush) texture = GetTexture();
            prevBrush = brush;
            if(brush != 0) prevUseBrush = brush;

            if(InputHelper.NewMouse.LeftButton == ButtonState.Released)
                return;

            else UseBrush(levelCode);
        }

        private void Inputs()
        {
            if(useEraser.Held()) brush = Brush.Eraser;
            else if(useEraser.Released()) brush = prevUseBrush;

            if(cycleBrush.Pressed()) brush++;
            if((int)brush > 2) brush = Brush.Grass;

            SetMouseWorldPos();
        }

        private void SetMouseWorldPos()
        {
            Vector2 refValue = InputHelper.NewMouse.Position.ToVector2();
            camera.ToWorld(ref refValue, out mouseWorldPos);

            if(mouseWorldPos.X < 0) mouseWorldPos.X = 0;
            if(mouseWorldPos.Y < 0) mouseWorldPos.Y = 0;
            mouseWorldPos -= screenSize * .5f;

            mouseWorldPos = VectorHelper.Floor(mouseWorldPos / tileSize) * tileSize;
        }

        private Texture2D GetTexture()
        {
            if(brush == Brush.Eraser) return null;
            return content.Load<Texture2D>("Sprites/Tiles/Tile" + (int)brush);
        }

        private void UseBrush(List<List<int>> levelCode)
        {
            if(!IsPosWithinBounds(mouseWorldPos, levelCode, out bool lessThanZero)) {
                if(lessThanZero || brush == Brush.Eraser) return;
                ExtendLevelToMouse(levelCode);
            }

            Vector2 tilePos = mouseWorldPos / tileSize;
            if(levelCode[tilePos.YInt()][tilePos.XInt()] != (int)brush)
                levelCode[tilePos.YInt()][tilePos.XInt()] = (int)brush;

            returnCode = levelCode;
        }

        private bool IsPosWithinBounds(Vector2 vector, List<List<int>> levelCode, out bool lessThanZero)
        {
            if(vector.X < 0 || vector.Y < 0) {
                lessThanZero = true;
                return false;
            }
            lessThanZero = false;

            if(vector.X >= levelCode[0].Count * tileSize || 
                vector.Y >= levelCode.Count * tileSize)
                return false;
            return true;
        }

        private void ExtendLevelToMouse(List<List<int>> levelCode)
        {
            while(!IsPosWithinBounds(mouseWorldPos, levelCode, out bool shouldReturn)) {
                if(shouldReturn) return;

                if(!IsPosWithinBounds(new(mouseWorldPos.X, 0), levelCode, out _))
                    for(int y = 0; y < levelCode.Count; y++)
                        levelCode[y].Add(0);
                if(!IsPosWithinBounds(new(0, mouseWorldPos.Y), levelCode, out _)) {
                    levelCode.Add(new());
                    for(int x = 0; x < levelCode[0].Count; x++)
                        levelCode[^1].Add(0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(brush == Brush.Eraser) return;
            if(texture == null) return;

            Rectangle rect = new(mouseWorldPos.ToPoint(), new(tileSize, tileSize));
            spriteBatch.Draw(texture, rect, Color.White * .7f);
        }
    }
}