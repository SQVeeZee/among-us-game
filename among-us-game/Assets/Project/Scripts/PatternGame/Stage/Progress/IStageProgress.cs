namespace PatternGame
{
    public interface IStageProgress
    {
        int CurrentStage { get; }
        void Increase();
        void Reset();
    }
}