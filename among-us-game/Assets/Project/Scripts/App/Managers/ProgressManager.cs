using JetBrains.Annotations;
using PatternGame;

namespace App
{
    [UsedImplicitly]
    public class ProgressManager : ILevelProgress, IStageProgress
    {
        public int CurrentLevel { get; private set; } = 1;
        public int CurrentLevelIndex => CurrentLevel - 1;
        public int CurrentStage { get; private set; } = 1;
        public int CurrentStageIndex => CurrentStage - 1;

        void ILevelProgress.Increase() => CurrentLevel++;
        void ILevelProgress.Reset()
        {
            CurrentLevel = 1;
            CurrentStage = 1;
        }

        void IStageProgress.Increase() => CurrentStage++;
        void IStageProgress.Reset() => CurrentStage = 1;
    }
}