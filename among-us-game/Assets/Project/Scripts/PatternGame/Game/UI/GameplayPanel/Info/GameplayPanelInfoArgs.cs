namespace PatternGame
{
    public struct GameplayPanelInfoArgs
    {
        public GameplayPanel Panel { get; }
        public int StageCount { get; }

        public GameplayPanelInfoArgs(GameplayPanel panel, int stageCount)
        {
            Panel = panel;
            StageCount = stageCount;
        }
    }
}