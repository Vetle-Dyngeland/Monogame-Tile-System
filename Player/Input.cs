namespace Tile_System.Player
{
    public class Input
    {
        private readonly ICondition right = new AnyCondition(
                new KeyboardCondition(Keys.Right),
                new KeyboardCondition(Keys.D));

        private readonly ICondition left = new AnyCondition(
                new KeyboardCondition(Keys.Left),
                new KeyboardCondition(Keys.A));

        private readonly ICondition up = new AnyCondition(
                new KeyboardCondition(Keys.Up),
                new KeyboardCondition(Keys.W));

        private readonly ICondition down = new AnyCondition(
                new KeyboardCondition(Keys.Down),
                new KeyboardCondition(Keys.S));

        public Vector2 moveVector;

        public void Update()
        {
            moveVector.X = KeyToInt(right, null) - KeyToInt(left, null);
            moveVector.Y = KeyToInt(up, null) - KeyToInt(down, null);
        }

        private int KeyToInt(ICondition key, string state)
        {
            return state switch {
                "down" => Convert.ToInt32(key.Pressed()),
                "up" => Convert.ToInt32(key.Released()),
                _ => Convert.ToInt32(key.Held())
            };
        }
    }
}
