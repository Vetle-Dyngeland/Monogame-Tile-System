using SharpDX.Direct3D9;
using System.Windows.Forms;
using Transform;

namespace Tile_System.Helpers
{
    public static class RectangleHelper
    {
        public static bool TouchingLeft(this Rectangle r1, Rectangle r2, Vector2 velocity)
        {
            return r1.Right + velocity.X > r2.Left &&
                    r1.Left < r2.Left &&
                    r1.Bottom > r2.Top &&
                    r1.Top < r2.Bottom;
        }

        public static bool TouchingRight(this Rectangle r1, Rectangle r2, Vector2 velocity)
        {
            return r1.Left + velocity.X < r2.Right &&
                    r1.Right > r2.Right &&
                    r1.Bottom > r2.Top &&
                    r1.Top < r2.Bottom;
        }

        public static bool TouchingTop(this Rectangle r1, Rectangle r2, Vector2 velocity)
        {
            return r1.Bottom + velocity.Y > r2.Top &&
                    r1.Top < r2.Top &&
                    r1.Right > r2.Left &&
                    r1.Left < r2.Right;
        }

        public static bool TouchingBottom(this Rectangle r1, Rectangle r2, Vector2 velocity)
        {
            return r1.Top + velocity.Y < r2.Bottom &&
                    r1.Bottom > r2.Bottom &&
                    r1.Right > r2.Left &&
                    r1.Left < r2.Right;
        }
    }
}
