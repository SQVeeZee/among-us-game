using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelController : ControllerWithResultBase
    {
        private readonly LevelModel _levelModel;
        private readonly LevelService _levelService;

        [Inject]
        private LevelController(
            IControllerFactory controllerFactory,
            LevelModel levelModel,
            LevelService levelService) : base(controllerFactory)
        {
            _levelModel = levelModel;
            _levelService = levelService;
        }

        protected override void OnStart()
        {
            ApplyCurrentLevel();

            Execute<LevelViewController>();
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<PatternPlaybackController>(cancellationToken);
            var result = await ExecuteAndWaitResultAsync<SelectSequenceController, SelectSequenceResult>(cancellationToken);
        }

        private void ApplyCurrentLevel()
        {
            var currentLevel = _levelService.GetCurrentLevel();
            _levelModel.ApplyCurrentLevel(currentLevel);
        }
    }
}