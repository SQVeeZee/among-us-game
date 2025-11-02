namespace MiniGames.SequenceRepeater
{
    public class VisualizationService
    {
        public bool IsVisualizing { get; private set; }

        public void SetVisualizingState(bool state)
        {
            IsVisualizing = state;
        }
    }
}