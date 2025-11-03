namespace PatternGame
{
    public interface IPatternProgress
    {
        void Increase();
        void Reset();
        bool IsLastStep();
    }
}