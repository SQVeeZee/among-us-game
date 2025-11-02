using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class GameLoopController : ControllerWithResultBase<GameLoopResult>
    {
        private readonly LevelService _levelService;

        [Inject]
        private GameLoopController(
            IControllerFactory controllerFactory,
            LevelService levelService,
            LevelModel levelModel)
            : base(controllerFactory)
            => _levelService = levelService;

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await RunLevelAndWaitForResultAsync(cancellationToken);
                HandleInputResult(result);
            }
        }

        private async UniTask<LevelResultType> RunLevelAndWaitForResultAsync(CancellationToken cancellationToken)
        {
            var currentLevel = _levelService.GetCurrentLevel();
            return await ExecuteAndWaitResultAsync<LevelController, LevelData, LevelResultType>(currentLevel, cancellationToken);
        }

        private void HandleInputResult(LevelResultType result)
        {
            switch (result)
            {
                case LevelResultType.Completed:
                    _levelService.Increase();
                    break;
                case LevelResultType.Fail:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}