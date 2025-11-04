using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class GameplayPanelInteractionController : ControllerWithResultBase<GameplayPanel, GameplayPanelResult>
    {
        private UniTaskCompletionSource<GameplayPanelResult> _panelCompletionSource;

        [Inject]
        private GameplayPanelInteractionController(
            IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }

        protected override void OnStart()
        {
            _panelCompletionSource = new UniTaskCompletionSource<GameplayPanelResult>();
            Args.Subscribe(HandleExitClick);
        }

        protected override void OnStop()
        {
            Args.Unsubscribe();
            _panelCompletionSource?.TrySetCanceled();
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await _panelCompletionSource.Task.AttachExternalCancellation(cancellationToken);
            Complete(result);
        }

        private void HandleExitClick(PanelResult result)
        {
            if (result == PanelResult.Exit)
            {
                TrySetResult(GameplayPanelResult.Exit);
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }

        private void TrySetResult(GameplayPanelResult result) => _panelCompletionSource?.TrySetResult(result);
    }
}