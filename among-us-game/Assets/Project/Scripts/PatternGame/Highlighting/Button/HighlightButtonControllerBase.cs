using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;

namespace PatternGame
{
    public abstract class HighlightButtonControllerBase : ControllerWithResultBase<int, EmptyControllerResult>
    {
        private readonly IGridContext _gridContext;
        private readonly HighlightConfig _config;

        protected HighlightButtonControllerBase(
            IControllerFactory controllerFactory,
            IGridContext gridContext,
            HighlightConfig config) : base(controllerFactory)
        {
            _gridContext = gridContext;
            _config = config;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var visualizationArgs = GetVisualizationArgs();
            await ExecuteAndWaitResultAsync<HighlightVisualizationController, HighlightVisualizationArgs, EmptyControllerResult>(visualizationArgs, cancellationToken);
            Complete(new EmptyControllerResult());
        }

        private HighlightVisualizationArgs GetVisualizationArgs()
        {
            var buttonView = GetButtonView(Args);
            return new HighlightVisualizationArgs(buttonView, _config);
        }

        private ButtonView GetButtonView(int id)
        {
            if (!_gridContext.TryGetItem(id, out var item))
            {
                throw new System.Exception($"Missing button for id={Args}");
            }
            return item.ButtonView;
        }
    }
}