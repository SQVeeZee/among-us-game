using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelService
    {
        private readonly LevelConfig _levelConfig;
        private int CurrentLevel { get; set; } = 1;
        private int CurrentLevelIndex => CurrentLevel - 1;

        [Inject]
        private LevelService(LevelConfig levelConfig) => _levelConfig = levelConfig;

        public LevelData GetCurrentLevel()
            => !IsOutOfRange() ? _levelConfig.GetLevelDataByIndex(CurrentLevelIndex) : GetRandomIndex();

        private bool IsOutOfRange() => CurrentLevel > _levelConfig.LevelsCount;

        private LevelData GetRandomIndex()
        {
            var index = UnityEngine.Random.Range(0, _levelConfig.LevelsCount);
            return _levelConfig.GetLevelDataByIndex(index);
        }

        public void Increase() => CurrentLevel++;
    }
}