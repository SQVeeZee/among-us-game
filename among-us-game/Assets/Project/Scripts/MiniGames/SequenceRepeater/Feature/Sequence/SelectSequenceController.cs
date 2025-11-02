using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public sealed class SelectSequenceController : ControllerWithResultBase<SelectSequenceResult>
    {
        private readonly GridContext _gridContext;
        private UniTaskCompletionSource<SelectSequenceResult> _tcs;

        [Inject]
        private SelectSequenceController(
            IControllerFactory controllerFactory,
            GridContext gridContext)
            : base(controllerFactory)
        {
            _gridContext = gridContext;
        }

        protected override void OnStart()
        {
            SubscribeOnButtons();
            _tcs = new UniTaskCompletionSource<SelectSequenceResult>();
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

        private void ButtonClickedHandle(int id) => HandleResult(id, CancellationToken).Forget();

        private async UniTaskVoid HandleResult(int id, CancellationToken cancellationToken)
        {
            var result = await ValidateTile(id, cancellationToken);
            switch (result)
            {
                case ValidationResult.Correct:
                    await ExecuteAndWaitResultAsync<CorrectSequenceController, int, bool>(id, cancellationToken);
                    return;
                case ValidationResult.Fail:
                    _tcs.TrySetResult(SelectSequenceResult.Fail);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTask<ValidationResult> ValidateTile(int id, CancellationToken cancellationToken)
        {
            var validationArgs = new ValidationArgs(id);
            return await ExecuteAndWaitResultAsync<ValidationController, ValidationArgs, ValidationResult>(validationArgs, cancellationToken);
        }
    }
}