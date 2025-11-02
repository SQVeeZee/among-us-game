using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class HighlightButtonController : ControllerWithResultBase<int, EmptyControllerResult>
    {
        private readonly GridContext _gridContext;
        private readonly HighlightConfig _config;

        protected HighlightButtonController(
            IControllerFactory controllerFactory,
            GridContext gridContext,
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