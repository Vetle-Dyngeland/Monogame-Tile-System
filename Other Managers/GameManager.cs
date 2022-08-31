namespace Tile_System
{
    public class GameManager
    {
        private readonly ContentManager content;

        private readonly FrameCounter fpsCounter;
        private readonly LevelManager levelManager;
        private readonly CameraManager cameraManager;
        private readonly Player.Main player;

        private readonly List<List<SpriteFont>> fonts = new();
        private readonly Camera camera;

        private const int tileSize = 32;
        private readonly Vector2 screenSize;

        public bool debugMode = false;

        #region Keypress IConditions
        private readonly ICondition debugModeKey = new AllCondition(
            new KeyboardCondition(Keys.LeftControl),
            new KeyboardCondition(Keys.D));
        #endregion Keypress IConditions

        public GameManager(ContentManager content, Vector2 screenSize, Camera camera)
        {
            this.content = content;
            this.screenSize = screenSize;
            this.camera = camera;

            levelManager = new(tileSize, camera, content, screenSize);
            player = new(tileSize);
            cameraManager = new(player, camera);
            fpsCounter = new();
        }

        public void LoadContent()
        {
            camera.LoadContent();
            player.Load(content);

            LoadFont(0, "Fonts/Arial24");
        }

        private void LoadFont(int index, string location)
        {
            while(fonts.Count <= index)
                fonts.Add(new());

            fonts[index].Add(content.Load<SpriteFont>(location));
        }

        public void Update(GameTime gameTime)
        {
            Inputs();

            levelManager.debugMode = debugMode;
            player.movement.useTestMovement = levelManager.levelCreatorMode;

            player.Update(gameTime, levelManager.map.CollisionTiles);
            levelManager.Update();

            if(!debugMode)
                cameraManager.Update(gameTime);
            else camera.Position = VectorHelper.Floor(player.position - player.rect.Size.ToVector2() / 2);
        }

        private void Inputs()
        {
            if(debugModeKey.Pressed()) debugMode = !debugMode;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            levelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);

            if(debugMode) {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                fpsCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", fpsCounter.averageFPS);
                spriteBatch.DrawString(fonts[0][0], fps, camera.Position - screenSize * .5f, Color.White);
            }
        }
    }
}