using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class BoardViewController : ControllerBase
    {
        private readonly IGameplayPanelContext _panelContext;
        private readonly BoardFactory _boardFactory;
        private readonly IBoardContext _boardContext;

        [Inject]
        private BoardViewController(
            IControllerFactory factory,
            IGameplayPanelContext panelContext,
            BoardFactory boardFactory,
            IBoardContext boardContext)
            : base(factory)
        {
            _panelContext = panelContext;
            _boardFactory = boardFactory;
            _boardContext = boardContext;
        }

        protected override void OnStart()
        {
            var panel = _panelContext.GameplayPanel;
            var boardView = _boardFactory.CreateBoard(panel.ContentRoot);
            _boardContext.Bind(boardView);
        }

        protected override void OnStop()
        {
            if (_boardContext.BoardView != null)
            {
                _boardFactory.Destroy(_boardContext.BoardView);
                _boardContext.UnBind();
            }
        }
    }
}