namespace Tile_System.Helpers
{
    public static class VectorHelper
    {
        public static int XInt(this Vector2 vector)
        {
            return (int)vector.X;
        }

        public static int YInt(this Vector2 vector)
        {
            return (int)vector.Y;
        }

        public static Vector2 Floor(this Vector2 vector)
        {
            return new(MathF.Floor(vector.X), MathF.Floor(vector.Y));
        }

        public static Vector2 Round(this Vector2 vector)
        {
            return new(MathF.Round(vector.X), MathF.Round(vector.Y));
        }

        public static Vector2 GetValue(this Vector2 vector)
        {
            return new(vector.X.GetValue(), vector.Y.GetValue());
        }

        public static Vector2 Average(this List<Vector2> vectorList)
        {
            float xSum = 0, ySum = 0;
            foreach(Vector2 vector in vectorList) {
                xSum += vector.X;
                ySum += vector.Y;
            }
            return new Vector2(xSum, ySum) / vectorList.Count;
        }


        public static Vector2 Normalized(this Vector2 vector)
        {
            float magnitude = Magnitude(vector);
            if(vector.X != 0) vector.X /= magnitude;
            if(vector.Y != 0) vector.Y /= magnitude;
            return vector;
        }

        public static float Magnitude(this Vector2 vector)
        {
            return (vector.X * vector.X) + (vector.Y * vector.Y);
        }
    }
}