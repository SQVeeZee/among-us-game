namespace MiniGames.SequenceRepeater
{
    public readonly struct SequenceProgressResult
    {
        public int CurrentStep { get; }

        public SequenceProgressResult(int currentStep) => CurrentStep = currentStep;
    }
}