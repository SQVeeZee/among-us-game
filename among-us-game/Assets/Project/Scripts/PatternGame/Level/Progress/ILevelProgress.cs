namespace App
{
    public interface ILevelProgress
    {
        int CurrentLevel { get; }
        void Increase();
        void Reset();
    }
}