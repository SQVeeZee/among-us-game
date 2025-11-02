using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class BoardViewController : ControllerBase
    {
        private readonly BoardContainer _container;
        private readonly BoardFactory _factory;
        private readonly BoardContext _boardContext;

        [Inject]
        private BoardViewController(
            IControllerFactory factory,
            BoardContainer container,
            BoardFactory boardFactory,
            BoardContext boardContext)
            : base(factory)
        {
            _container = container;
            _factory = boardFactory;
            _boardContext = boardContext;
        }

        protected override void OnStart()
        {
            var boardView = _factory.CreateBoard(_container.Root);
            _boardContext.Bind(boardView);
        }
    }
}