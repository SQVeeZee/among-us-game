
namespace MiniGames.SequenceRepeater
{
    public class LevelModel
    {
        private readonly LevelConfig _levelConfig;

        private LevelData _levelData;
        private int _levelProgress;

        public void ApplyCurrentLevel(LevelData levelData) => _levelData = levelData;

        public void Increase() => _levelProgress++;
        public void Reset() => _levelProgress = 0;

        public int GetTilesCount() => _levelData.TilesCount;
        public LevelSequence GetLevelSequence() => _levelData.LevelSequences[_levelProgress];
        public bool IsLastStep() => _levelProgress == _levelData.LevelSequences.Length - 1;
    }
}