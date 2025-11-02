using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class HighlightVisualizationController : VisualizationControllerBase<IHighlighted, EmptyControllerResult>
    {
        private readonly HighlightVisualization _highlightVisualization;
        private readonly HighlightConfig _highlightConfig;

        [Inject]
        private HighlightVisualizationController(
            IControllerFactory controllerFactory,
            VisualizationService visualizationService,
            HighlightVisualization highlightVisualization,
            HighlightConfig highlightConfig) : base(controllerFactory, visualizationService)
        {
            _highlightVisualization = highlightVisualization;
            _highlightConfig = highlightConfig;
        }

        protected override async UniTask OnVisualizing(CancellationToken cancellationToken)
        {
            await _highlightVisualization.Blink(Args, _highlightConfig, CancellationToken);
            Complete(new EmptyControllerResult());
        }
    }
}