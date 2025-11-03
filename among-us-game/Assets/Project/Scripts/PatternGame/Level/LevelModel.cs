using JetBrains.Annotations;

namespace PatternGame
{
    [UsedImplicitly]
    public sealed class LevelModel
    {
        private StageData[] _stages;

        public void ApplyCurrentLevel(StageData[] stages) => _stages = stages;

        public StageData GetStageData(int stageNumber) => _stages[stageNumber - 1];
        public bool IsLastStage(int stageNumber) => stageNumber == _stages.Length;
    }
}