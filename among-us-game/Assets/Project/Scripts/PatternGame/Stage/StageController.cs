using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class StageController : ControllerWithResultBase<StageArgs, StageResult>
    {
        [Inject]
        private StageController(IControllerFactory controllerFactory) : base(controllerFactory)
        {

        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteSequenceAndWaitForResultAsync(cancellationToken);
            Complete(result);
        }

        private async UniTask<StageResult> ExecuteSequenceAndWaitForResultAsync(CancellationToken cancellationToken)
        {
            var result = await ExecutePatternAndWaitForResultAsync(cancellationToken);
            return result switch
            {
                PatternResult.Completed => StageResult.Completed,
                PatternResult.Failed => StageResult.Failed,
                PatternResult.Continue => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private async UniTask<PatternResult> ExecutePatternAndWaitForResultAsync(CancellationToken cancellationToken)
        {
            var tiles = Args.TilesCount;
            var length = Args.PatternData.Length;
            var args = new PatternArgs(tiles, length);
            return await ExecuteAndWaitResultAsync<PatternController, PatternArgs, PatternResult>(args, cancellationToken);
        }
    }
}