namespace PatternGame
{
    public readonly struct HighlightVisualizationArgs
    {
        public IHighlighted Highlighted { get; }
        public HighlightConfig Config { get; }

        public HighlightVisualizationArgs(IHighlighted highlighted, HighlightConfig config)
        {
            Highlighted = highlighted;
            Config = config;
        }
    }
}