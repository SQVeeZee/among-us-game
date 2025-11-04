using App;
using VContainer;

namespace PatternGame
{
    public class LevelManager
    {
        private readonly ProgressManager _progressManager;
        private readonly LevelConfig _levelConfig;

        [Inject]
        private LevelManager(
            ProgressManager progressManager,
            LevelConfig levelConfig)
        {
            _progressManager = progressManager;
            _levelConfig = levelConfig;
        }

        public LevelData GetCurrentLevel() => !IsOutOfRange() ? _levelConfig.GetLevelDataByIndex(_progressManager.CurrentLevelIndex) : GetRandomIndex();

        private bool IsOutOfRange() => _progressManager.CurrentLevel > _levelConfig.LevelsCount;

        private LevelData GetRandomIndex()
        {
            var index = UnityEngine.Random.Range(0, _levelConfig.LevelsCount);
            return _levelConfig.GetLevelDataByIndex(index);
        }
    }
}