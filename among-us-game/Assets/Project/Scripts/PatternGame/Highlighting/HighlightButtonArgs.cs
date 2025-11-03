namespace PatternGame
{
    public readonly struct HighlightButtonArgs
    {
        public int Id { get; }
        public HighlightConfig Config { get; }

        public HighlightButtonArgs(int id, HighlightConfig config)
        {
            Id = id;
            Config = config;
        }
    }
}