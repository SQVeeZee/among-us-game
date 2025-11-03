namespace PatternGame
{
    public readonly struct GridItemPair
    {
        public int Id { get; }
        public TileView TileView { get; }
        public ButtonView ButtonView { get; }

        public GridItemPair(int id, TileView tileView, ButtonView buttonView)
        {
            Id = id;
            TileView = tileView;
            ButtonView = buttonView;
        }
    }
}