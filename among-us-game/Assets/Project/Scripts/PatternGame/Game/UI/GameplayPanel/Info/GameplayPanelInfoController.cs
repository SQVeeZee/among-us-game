using App;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class GameplayPanelInfoController : ControllerBase<GameplayPanelInfoArgs>
    {
        private readonly ProgressManager _progressManager;

        [Inject]
        private GameplayPanelInfoController(
            IControllerFactory controllerFactory,
            ProgressManager progressManager)
            : base(controllerFactory) => _progressManager = progressManager;

        protected override void OnStart()
        {
            UpdateStageInfo();
            Subscribe();
        }

        protected override void OnStop() => Dispose();

        private void Subscribe() => _progressManager.OnStageUpdated += StageUpdatedHandle;
        private void Dispose() => _progressManager.OnStageUpdated -= StageUpdatedHandle;

        private void StageUpdatedHandle(int stageNumber) => UpdateStageInfo();

        private void UpdateStageInfo()
        {
            var panel = Args.Panel;
            var stageInfo = (_progressManager.CurrentStage, Args.StageCount);
            panel.UpdateLevelInfo(_progressManager.CurrentLevel, stageInfo);
        }
    }
}