namespace PatternGame
{
    public readonly struct PatternArgs
    {
        public int Tiles { get; }
        public int Length { get; }

        public PatternArgs(int tiles, int length)
        {
            Tiles = tiles;
            Length = length;
        }
    }
}