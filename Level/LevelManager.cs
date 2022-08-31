using System.Collections.Generic;
using System.Linq;

namespace Tile_System.Level
{
    public class LevelManager
    {
        readonly LevelCreator levelCreator;
        public List<List<int>> levelCode = new() { new()};
        public bool debugMode = true;

        public Map map;
        private readonly int tileSize = 32;

        public bool levelCreatorMode;

        private readonly ICondition useLevelCreatorButton = new AllCondition(
            new KeyboardCondition(Keys.LeftControl),
            new KeyboardCondition(Keys.E));

        public LevelManager(int tileSize, Camera camera, ContentManager content, Vector2 screenSize)
        {
            this.tileSize = tileSize;
            map = new();

            levelCode = new() {
                new(){0,0,0,0,0,0,0,0,0},
                new(){0,0,0,0,0,0,1,0,1},
                new(){1,0,0,0,0,1,2,1,2},
                new(){2,1,1,1,0,2,2,2,2},
                new(){2,2,2,2,1,2,2,2,2},
                new(){2,2,2,2,2,2,2,2,2},
                new(){2,2,2,2,2,2,2,2,2},
            };

            levelCreator = new(camera, tileSize, content, screenSize);
        }

        public void Update()
        {
            if(useLevelCreatorButton.Pressed()) levelCreatorMode = !levelCreatorMode;
            levelCreator.canEditLevel = levelCreatorMode;

            levelCreator.Update(levelCode);
            levelCode = levelCreator.returnCode;

            RemoveLastLines();

            map.Generate(LevelToArray(), tileSize, debugMode);
        }

        #region RemoveLines
        private void RemoveLastLines()
        {
            if(levelCode == null || levelCode.Count <= 0) return;
            if(levelCode[0] == null || levelCode[0].Count <= 0) return;

            CheckRemoveXLine();
            CheckRemoveYLine();
        }

        private void CheckRemoveXLine()
        {
            while(levelCode[0].Count > 0) {
                for(int y = 0; y < levelCode.Count; y++)
                    if(levelCode[y][^1] != 0) return;
                RemoveXLine(levelCode[0].Count - 1);
            }
        }

        private void RemoveXLine(int index)
        {
            for(int y = 0; y < levelCode.Count; y++)
                levelCode[y].RemoveAt(index);   
        }

        private void CheckRemoveYLine()
        {
            while(levelCode.Count > 0) {
                for(int x = 0; x < levelCode[^1].Count; x++)
                    if(levelCode[^1][x] != 0) return;
                RemoveYLine(levelCode.Count - 1);
            }
        }

        private void RemoveYLine(int index) => levelCode.RemoveAt(index);
        #endregion 

        private int[,] LevelToArray()
        {
            return levelCode.Select(Enumerable.ToArray).ToArray().To2D();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            if(levelCreatorMode)
                levelCreator.Draw(spriteBatch);
        }
    }
}