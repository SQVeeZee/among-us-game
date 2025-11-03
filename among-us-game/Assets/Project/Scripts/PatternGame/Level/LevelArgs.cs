namespace PatternGame
{
    public readonly struct LevelArgs
    {
        public int TilesCount { get; }
        public StageData[] Stages { get; }

        public LevelArgs(int tilesCount, StageData[] stages)
        {
            TilesCount = tilesCount;
            Stages = stages;
        }
    }
}