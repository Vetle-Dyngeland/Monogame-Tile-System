namespace Tile_System
{
    public class CameraManager
    {
        private readonly List<Vector2> positions = new();
        private readonly Player.Main player;
        private readonly Camera camera;

        private readonly int lookaheadTime = 250;
        private readonly int smoothingAmount = 25;

        public bool ignoreYLookahead = false;

        public CameraManager(Player.Main player, Camera camera)
        {
            this.player = player;
            this.camera = camera;

            if(smoothingAmount < 1) smoothingAmount = 1;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 addPos;
            if(ignoreYLookahead)
                addPos = player.position - player.rect.Size.ToVector2() / 2 +
                    player.movement.velocity.X * Vector2.UnitX * deltaTime * lookaheadTime;
            else
                addPos = player.position +
                    player.movement.velocity * deltaTime * lookaheadTime;
            positions.Add(addPos);

            if(positions.Count > smoothingAmount)
                positions.RemoveAt(0);

            camera.Position = VectorHelper.Floor(positions.Average());
            camera.Update(gameTime);
        }
    }
}