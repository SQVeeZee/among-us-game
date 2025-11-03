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
        private readonly LevelManager _levelManager;

        [Inject]
        private ModuleController(
            IControllerFactory factory,
            LevelManager levelManager)
            : base(factory)
            => _levelManager = levelManager;

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteLevelAsync(cancellationToken);
            Complete(result);
        }

        private async UniTask<ModuleResult> ExecuteLevelAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteLevelAndWaitForResultAsync(cancellationToken);
            switch (result)
            {
                case LevelResult.Completed:
                    Execute<IncreaseLevelProgressController>();
                    return ModuleResult.Completed;
                case LevelResult.Fail:
                    return ModuleResult.Failed;
                case LevelResult.Exit:
                    return ModuleResult.Exit;
                case LevelResult.None:
                    return ModuleResult.Unknown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTask<LevelResult> ExecuteLevelAndWaitForResultAsync(CancellationToken cancellationToken)
        {
            var level = _levelManager.GetCurrentLevel();
            var args = new LevelArgs(level.TilesCount, level.Stages);
            return await ExecuteAndWaitResultAsync<LevelController, LevelArgs, LevelResult>(args, cancellationToken);
        }
    }
}