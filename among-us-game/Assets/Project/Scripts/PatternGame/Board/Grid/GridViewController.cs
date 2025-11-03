using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class GridViewController : ControllerBase<int>
    {
        private readonly IGridContext _gridContext;
        private readonly GridFactory _gridFactory;

        [Inject]
        private GridViewController(
            IControllerFactory controllerFactory,
            IGridContext gridContext,
            GridFactory gridFactory)
            : base(controllerFactory)
        {
            _gridContext = gridContext;
            _gridFactory = gridFactory;
        }

        protected override void OnStart()
        {
            var tilesCount = Args;
            for (var i = 0; i < tilesCount; i++)
            {
                var id = GetItemId(i);
                CreateAndCacheItemPair(id);
            }
        }

        protected override void OnStop()
        {
            foreach (var itemPair in _gridContext.Items)
            {
                DestroyPair(itemPair);
            }
            _gridContext.Clean();
        }

        private void CreateAndCacheItemPair(int id)
        {
            var tileView = _gridFactory.CreateTileView(_gridContext.TileRoot);
            var buttonView = _gridFactory.CreateButtonView(_gridContext.ButtonRoot);
            var itemPair = new GridItemPair(id, tileView, buttonView);
            _gridContext.Add(itemPair);
        }

        private void DestroyPair(GridItemPair itemPair)
        {
            _gridFactory.DestroyTileView(itemPair.TileView);
            _gridFactory.DestroyButtonView(itemPair.ButtonView);
        }

        private static int GetItemId(int index) => index;
    }
}