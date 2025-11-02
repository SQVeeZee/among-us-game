using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class GridViewController : ControllerBase
    {
        private readonly BoardContext _boardContext;
        private readonly GridContext _gridContext;
        private readonly GridFactory _gridFactory;
        private readonly GridModel _gridModel;
        private readonly LevelModel _levelModel;

        [Inject]
        private GridViewController(
            IControllerFactory controllerFactory,
            BoardContext boardContext,
            GridContext gridContext,
            GridFactory gridFactory,
            GridModel gridModel,
            LevelModel levelModel)
            : base(controllerFactory)
        {
            _boardContext = boardContext;
            _gridContext = gridContext;
            _gridFactory = gridFactory;
            _gridModel = gridModel;
            _levelModel = levelModel;
        }

        protected override void OnStart() => FillBoard();

        private void FillBoard()
        {
            var tilesCount = _levelModel.GetTilesCount();
            for (var i = 0; i < tilesCount; i++)
            {
                var id = _gridModel.GetItemId(i);
                CreateAndCacheItemPair(id);
            }
        }

        private void CreateAndCacheItemPair(int id)
        {
            var boardView = _boardContext.BoardView;
            var tileView = _gridFactory.CreateTileView(boardView.TileRoot);
            var buttonView = _gridFactory.CreateButtonView(boardView.ButtonRoot);
            var itemPair = new GridItemPair(id, tileView, buttonView);
            _gridContext.Add(itemPair);
        }
    }
}