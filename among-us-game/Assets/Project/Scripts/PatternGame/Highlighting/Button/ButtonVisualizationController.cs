using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ButtonVisualizationController : ControllerWithResultBase<ButtonVisualizationArgs, EmptyControllerResult>
    {
        [Inject]
        private ButtonVisualizationController(
            IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await VisualizeClickAsync(Args.ID, Args.ClickResult, cancellationToken);
            Complete(new EmptyControllerResult());
        }

        private async UniTask VisualizeClickAsync(int id, ClickResult result, CancellationToken cancellationToken)
        {
            switch (result)
            {
                case ClickResult.Correct:
                    await ExecuteAndWaitResultAsync<CorrectHighlightController, int, EmptyControllerResult>(id, cancellationToken);
                    break;
                case ClickResult.Wrong:
                    await ExecuteAndWaitResultAsync<WrongHighlightController, int, EmptyControllerResult>(id, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}