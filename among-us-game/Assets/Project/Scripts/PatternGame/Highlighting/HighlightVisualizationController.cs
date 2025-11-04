using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class HighlightVisualizationController : ControllerWithResultBase<HighlightVisualizationArgs, EmptyControllerResult>
    {
        private readonly HighlightVisualizer _highlightVisualizer;

        [Inject]
        private HighlightVisualizationController(
            IControllerFactory controllerFactory,
            HighlightVisualizer highlightVisualizer)
            : base(controllerFactory)
            => _highlightVisualizer = highlightVisualizer;

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await BlinkAsync(Args.Highlighted, Args.Config, cancellationToken);
            Complete(new EmptyControllerResult());
        }

        private async UniTask BlinkAsync(IHighlighted highlighted, HighlightConfig config, CancellationToken cancellationToken)
            => await _highlightVisualizer.BlinkAsync(highlighted, config, cancellationToken);
    }
}