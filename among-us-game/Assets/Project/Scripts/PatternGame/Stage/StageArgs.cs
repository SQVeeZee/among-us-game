namespace PatternGame
{
    public readonly struct StageArgs
    {
        public PatternData PatternData { get; }
        public int TilesCount { get; }

        public StageArgs(int tilesCount, PatternData data)
        {
            PatternData = data;
            TilesCount = tilesCount;
        }
    }
}