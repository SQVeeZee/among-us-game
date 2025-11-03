namespace PatternGame
{
    public interface IBoardContext
    {
        BoardView BoardView { get; }
        void Bind(BoardView view);
        void UnBind();
    }
}