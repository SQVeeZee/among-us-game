namespace MiniGames.SequenceRepeater
{
    public class BoardContext
    {
        public BoardView BoardView { get; private set; }

        public void Bind(BoardView view)
        {
            BoardView = view;
        }

        public void UnBind()
        {
            BoardView = null;
        }
    }
}