
namespace MiniGames.SequenceRepeater
{
    public class LevelModel
    {
        private readonly LevelConfig _levelConfig;
        private LevelData _levelData;

        public int LevelProgress { get; private set; } = 0;

        public void ApplyCurrentLevel(LevelData levelData) => _levelData = levelData;

        public void Increase() => LevelProgress++;
        public void Reset() => LevelProgress = 0;

        public int GetTilesCount() => _levelData.TilesCount;
        public int StageCount() => _levelData.LevelSequences.Length;
        public LevelSequence GetLevelSequence() => _levelData.LevelSequences[LevelProgress];
    }
}