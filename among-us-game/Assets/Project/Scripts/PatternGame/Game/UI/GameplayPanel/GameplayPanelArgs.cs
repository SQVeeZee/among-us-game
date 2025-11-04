namespace PatternGame
{
    public struct GameplayPanelArgs
    {
        public int StageCount { get; }

        public GameplayPanelArgs(int stageCount)
        {
            StageCount = stageCount;
        }
    }
}