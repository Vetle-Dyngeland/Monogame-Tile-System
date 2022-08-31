namespace Tile_System.Helpers
{
    public static class NumberHelper
    {
        public static int GetValue(this float f)
        {
            if(f == 0) return (int)f;
            return (int)(MathF.Abs(f) / f);
        }

        public static float Average(this List<float> list)
        {
            float sum = 0;
            foreach(var f in list)
                sum += f;
            return sum / list.Count;
        }
    }
}