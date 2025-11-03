namespace PatternGame
{
    public struct ButtonVisualizationArgs
    {
        public int ID { get; }
        public ClickResult ClickResult { get; }

        public ButtonVisualizationArgs(int id, ClickResult clickResult)
        {
            ID = id;
            ClickResult = clickResult;
        }
    }
}