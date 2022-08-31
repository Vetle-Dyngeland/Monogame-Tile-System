namespace Tile_System
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Camera camera;
        private GameManager gameManager;

        private Vector2 screenSize = new(1600, 900);
        public bool isFullScreen = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InitializeScreenSettings();
            camera = new(graphics.GraphicsDevice);

            gameManager = new(Content, screenSize, camera);

            base.Initialize();
        }

        private void InitializeScreenSettings()
        {
            if(isFullScreen)
                screenSize = new(1920, 1080);
            else if(screenSize == default)
                screenSize = new(1600, 900);

            graphics.PreferredBackBufferHeight = screenSize.YInt();
            graphics.PreferredBackBufferWidth = screenSize.XInt();
            graphics.IsFullScreen = isFullScreen;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            InputHelper.Setup(this);

            Tile.Content = Content;
            gameManager.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHelper.UpdateSetup();
            if(InputHelper.NewKeyboard.IsKeyDown(Keys.Escape))
                Exit();

            gameManager.Update(gameTime);

            InputHelper.UpdateCleanup();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(camera);

            gameManager.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}