using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;

namespace MiniGames.SequenceRepeater
{
    public abstract class VisualizationControllerBase : ControllerWithResultBase
    {
        private readonly VisualizationService _visualizationService;

        protected VisualizationControllerBase(
            IControllerFactory controllerFactory,
            VisualizationService visualizationService)
            : base(controllerFactory)
            => _visualizationService = visualizationService;

        protected sealed override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            _visualizationService.SetVisualizingState(true);
            await OnVisualizing(cancellationToken);
            _visualizationService.SetVisualizingState(false);
        }

        protected abstract UniTask OnVisualizing(CancellationToken cancellationToken);
    }

    public abstract class VisualizationControllerBase<TResult> : ControllerWithResultBase<TResult>
    {
        private readonly VisualizationService _visualizationService;

        protected VisualizationControllerBase(
            IControllerFactory controllerFactory,
            VisualizationService visualizationService)
            : base(controllerFactory)
            => _visualizationService = visualizationService;

        protected sealed override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            _visualizationService.SetVisualizingState(true);
            await OnVisualizing(cancellationToken);
            _visualizationService.SetVisualizingState(false);
        }

        protected abstract UniTask OnVisualizing(CancellationToken cancellationToken);
    }

    public abstract class VisualizationControllerBase<TArg, TResult> : ControllerWithResultBase<TArg, TResult>
    {
        private readonly VisualizationService _visualizationService;

        protected VisualizationControllerBase(
            IControllerFactory controllerFactory,
            VisualizationService visualizationService)
            : base(controllerFactory)
            => _visualizationService = visualizationService;

        protected sealed override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            _visualizationService.SetVisualizingState(true);
            await OnVisualizing(cancellationToken);
            _visualizationService.SetVisualizingState(false);
        }

        protected abstract UniTask OnVisualizing(CancellationToken cancellationToken);
    }
}