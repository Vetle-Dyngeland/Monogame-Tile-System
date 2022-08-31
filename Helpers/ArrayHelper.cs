using System.Linq;

namespace Tile_System.Helpers
{
    public static class ArrayHelper
    {
        public static T[,] To2D<T>(this T[][] jagged)
        {
            try {
                int dim1 = jagged.Length;
                int dim2 = jagged.GroupBy(row => row.Length).Single().Key;

                var result = new T[dim1, dim2];
                for(int i = 0; i < dim1; ++i)
                    for(int j = 0; j < dim2; ++j)
                        result[i, j] = jagged[i][j];

                return result;
            }
            catch(InvalidOperationException) {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }
    }
}