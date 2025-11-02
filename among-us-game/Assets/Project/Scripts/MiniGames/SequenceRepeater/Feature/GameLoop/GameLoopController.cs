using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class GameLoopController : ControllerWithResultBase<GameLoopResult>
    {
        [Inject]
        private GameLoopController(IControllerFactory controllerFactory) : base(controllerFactory)
        { }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<LevelController>(cancellationToken);
        }
    }
}