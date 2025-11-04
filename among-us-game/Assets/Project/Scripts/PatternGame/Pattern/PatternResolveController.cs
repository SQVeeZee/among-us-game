using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class PatternResolveController : ControllerWithResultBase<EmptyControllerArg, PatternResult>
    {
        private readonly IGridContext _gridContext;

        private UniTaskCompletionSource<PatternResult> _sequenceCompletionSource;

        [Inject]
        private PatternResolveController(
            IControllerFactory controllerFactory,
            IGridContext gridContext)
            : base(controllerFactory)
            => _gridContext = gridContext;

        protected override void OnStart()
        {
            _sequenceCompletionSource = new UniTaskCompletionSource<PatternResult>();
            Subscribe();
        }

        protected override void OnStop() => Dispose();

        private void Subscribe()
        {
            foreach (var item in _gridContext.Items)
            {
                var id = item.Id;
                item.ButtonView.Subscribe(() => ButtonClickedHandle(id));
            }
        }

        private void Dispose()
        {
            foreach (var item in _gridContext.Items)
            {
                item.ButtonView.Dispose();
            }
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await _sequenceCompletionSource.Task.AttachExternalCancellation(cancellationToken);
            Complete(result);
        }

        private void ButtonClickedHandle(int id) => ExecuteClickAsync(id, CancellationToken).Forget();

        private async UniTask ExecuteClickAsync(int id, CancellationToken cancellationToken)
        {
            var clickResult = await ExecuteAndWaitResultAsync<ClickValidationController, ValidationArgs, ClickResult>(new ValidationArgs(id), cancellationToken);
            var result = await ExecuteAndWaitResultAsync<PatternProgressController, ClickResult, PatternResult>(clickResult, cancellationToken);
            var isFinished = result == PatternResult.Completed || result == PatternResult.Failed;
            if (isFinished)
            {
                Dispose();
            }
            await ExecuteClickVisualizationAsync(id, clickResult, cancellationToken);
            if (isFinished)
            {
                TrySetResult(result);
            }
        }

        private async UniTask ExecuteClickVisualizationAsync(int id, ClickResult clickResult, CancellationToken cancellationToken)
        {
            var args = new ButtonVisualizationArgs(id, clickResult);
            await ExecuteAndWaitResultAsync<ButtonVisualizationController, ButtonVisualizationArgs, EmptyControllerResult>(args, cancellationToken);
        }

        private void TrySetResult(PatternResult result) => _sequenceCompletionSource?.TrySetResult(result);
    }
}