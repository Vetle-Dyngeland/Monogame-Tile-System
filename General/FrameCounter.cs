using System.Linq;

namespace Tile_System.General
{
    public class FrameCounter
    {
        public float averageFPS;
        public float currentFPS;
        public int roundAmount = 100;

        private List<float> fpsList = new();

        public void Update(float deltaTime)
        {
            currentFPS = 1 / deltaTime;

            fpsList.Add(currentFPS);
            if(fpsList.Count > roundAmount)
                fpsList.RemoveAt(0);

            averageFPS = fpsList.Average();
        }
    }
}