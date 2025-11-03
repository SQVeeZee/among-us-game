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

        protected override void OnStart() => Subscribe();

        protected override void OnStop()
        {
            TrySetResult(PatternResult.None);
            DisposeA();
        }

        private void Subscribe()
        {
            _sequenceCompletionSource = new UniTaskCompletionSource<PatternResult>();
            foreach (var item in _gridContext.Items)
            {
                var id = item.Id;
                item.ButtonView.Subscribe(() => ButtonClickedHandle(id));
            }
        }

        private void DisposeA()
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
            if (result != PatternResult.None)
            {
                DisposeA();
            }
            await ExecuteClickVisualization(id, clickResult, cancellationToken);
            if (result != PatternResult.None)
            {
                TrySetResult(result);
            }
        }

        private async UniTask ExecuteClickVisualization(int id, ClickResult clickResult, CancellationToken cancellationToken)
        {
            var args = new ButtonVisualizationArgs(id, clickResult);
            await ExecuteAndWaitResultAsync<ButtonVisualizationController, ButtonVisualizationArgs, EmptyControllerResult>(args, cancellationToken);
        }

        private void TrySetResult(PatternResult result) => _sequenceCompletionSource?.TrySetResult(result);
    }
}