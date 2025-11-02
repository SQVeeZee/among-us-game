using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class CorrectSequenceController : ControllerWithResultBase<int, bool>
    {
        [Inject]
        private CorrectSequenceController(
            IControllerFactory controllerFactory)
            : base(controllerFactory)
        {

        }

        protected override void OnStart()
        {

        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            Execute<IncreaseSequenceProgressController>();
            await ExecuteAndWaitResultAsync<HighlightButtonController, int, bool>(Args, cancellationToken);
        }
    }
}