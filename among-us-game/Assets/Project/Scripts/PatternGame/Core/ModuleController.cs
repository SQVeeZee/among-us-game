using System;
using System.Threading;
using App;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ModuleController : ControllerWithResultBase<EmptyControllerArg, ModuleResult>
    {
        [Inject]
        private ModuleController(
            IControllerFactory factory)
            : base(factory)
        {

        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteGameAndWaitResultAsync(cancellationToken);
            Complete(result);
        }

        private async UniTask<ModuleResult> ExecuteGameAndWaitResultAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<GameController, EmptyControllerArg, GameResult>(new EmptyControllerArg(), cancellationToken);
            return result switch
            {
                GameResult.Completed => ModuleResult.Completed,
                GameResult.Failed => ModuleResult.Failed,
                GameResult.Exit => ModuleResult.Exit,
                GameResult.Unknown => ModuleResult.Unknown,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}