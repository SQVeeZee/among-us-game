using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class GameController : ControllerWithResultBase<EmptyControllerArg, GameResult>
    {
        private readonly LevelManager _levelManager;

        [Inject]
        private GameController(
            IControllerFactory controllerFactory,
            LevelManager levelManager)
            : base(controllerFactory)
            => _levelManager = levelManager;

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteGameAsync(cancellationToken);
            Complete(result);
        }

        private async UniTask<GameResult> ExecuteGameAsync(CancellationToken cancellationToken)
        {
            using var gameCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var levelData = _levelManager.GetCurrentLevel();
            var finishedIndex = await UniTask.WhenAny(
                ExecuteGameplayPanelAsync(levelData, gameCancellationToken.Token),
                ExecuteLevelAsync(levelData, gameCancellationToken.Token));

            return finishedIndex.winArgumentIndex switch
            {
                0 => finishedIndex.result1,
                1 => finishedIndex.result2,
                _ => GameResult.Unknown
            };
        }

        private async UniTask<GameResult> ExecuteGameplayPanelAsync(LevelData levelData, CancellationToken cancellationToken)
        {
            var args = new GameplayPanelArgs(levelData.Stages.Length);
            var result = await ExecuteAndWaitResultAsync<GameplayPanelController, GameplayPanelArgs, GameplayPanelResult>(args, cancellationToken);
            return result switch
            {
                GameplayPanelResult.Exit => GameResult.Exit,
                _ => GameResult.Unknown
            };
        }

        private async UniTask<GameResult> ExecuteLevelAsync(LevelData levelData, CancellationToken cancellationToken)
        {
            var args = new LevelArgs(levelData.TilesCount, levelData.Stages);
            var result = await ExecuteAndWaitResultAsync<LevelController, LevelArgs, LevelResult>(args, cancellationToken);

            switch (result)
            {
                case LevelResult.Completed:
                    Execute<IncreaseLevelProgressController>();
                    return GameResult.Exit;
                case LevelResult.Failed:
                    return GameResult.Failed;
                case LevelResult.Exit:
                    return GameResult.Exit;
                default:
                    return GameResult.Unknown;
            }
        }
    }
}
