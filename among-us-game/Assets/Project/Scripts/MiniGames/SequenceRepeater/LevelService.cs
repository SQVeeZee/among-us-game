using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelService
    {
        private readonly LevelConfig _levelConfig;
        private int CurrentLevel { get; set; } = 1;

        [Inject]
        private LevelService(LevelConfig levelConfig) => _levelConfig = levelConfig;

        public LevelData GetCurrentLevel() => _levelConfig.GetCurrentLevelData(CurrentLevel);
    }
}