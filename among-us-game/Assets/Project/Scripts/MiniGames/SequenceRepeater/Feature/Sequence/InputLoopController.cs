using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public sealed class InputLoopController : ControllerWithResultBase<PatternProgressResult>
    {
        private readonly GridContext _gridContext;
        private UniTaskCompletionSource<PatternProgressResult> _tcs;

        [Inject]
        private InputLoopController(
            IControllerFactory controllerFactory,
            GridContext gridContext)
            : base(controllerFactory)
            => _gridContext = gridContext;

        protected override void OnStart()
        {
            SubscribeOnButtons();
            _tcs = new UniTaskCompletionSource<PatternProgressResult>();
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await _tcs.Task.AttachExternalCancellation(cancellationToken);
            Complete(result);
        }

        private void SubscribeOnButtons()
        {
            foreach (var item in _gridContext.Items)
            {
                var id = item.Id;
                item.ButtonView.Subscribe(() => ButtonClickedHandle(id));
            }
        }

        protected override void OnStop()
        {
            foreach (var item in _gridContext.Items)
            {
                item.ButtonView.Dispose();
            }
        }

        private void ButtonClickedHandle(int id) => ExecuteClickAsync(id, CancellationToken).Forget();

        private async UniTask ExecuteClickAsync(int id, CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<ClickValidationController, ValidationArgs, ValidationResult>(new ValidationArgs(id), cancellationToken);
            var patternResult = await ExecuteAndWaitResultAsync<SequenceProgressController, ValidationResult, PatternProgressResult>(result, cancellationToken);
            await VisualizeClickAsync(id, patternResult, cancellationToken);

            switch (patternResult)
            {
                case PatternProgressResult.Continue:
                    break;
                case PatternProgressResult.Success:
                case PatternProgressResult.Fail:
                    _tcs.TrySetResult(patternResult);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTask VisualizeClickAsync(int id, PatternProgressResult result, CancellationToken cancellationToken)
        {
            switch (result)
            {
                case PatternProgressResult.Success:
                case PatternProgressResult.Continue:
                    await ExecuteAndWaitResultAsync<CorrectHighlightController, int, EmptyControllerResult>(id, cancellationToken);
                    break;
                case PatternProgressResult.Fail:
                    await ExecuteAndWaitResultAsync<WrongHighlightController, int, EmptyControllerResult>(id, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}