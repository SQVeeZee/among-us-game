using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class BoardViewController : ControllerBase
    {
        private readonly BoardContainer _container;
        private readonly BoardFactory _boardFactory;
        private readonly IBoardContext _boardContext;

        [Inject]
        private BoardViewController(
            IControllerFactory factory,
            BoardContainer container,
            BoardFactory boardFactory,
            IBoardContext boardContext)
            : base(factory)
        {
            _container = container;
            _boardFactory = boardFactory;
            _boardContext = boardContext;
        }

        protected override void OnStart()
        {
            var boardView = _boardFactory.CreateBoard(_container.Root);
            _boardContext.Bind(boardView);
        }

        protected override void OnStop()
        {
            _boardFactory.Destroy(_boardContext.BoardView);
            _boardContext.UnBind();
        }
    }
}