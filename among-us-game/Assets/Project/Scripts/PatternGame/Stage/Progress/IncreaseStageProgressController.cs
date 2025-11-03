using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class IncreaseStageProgressController : StageProgressControllerBase
    {
        [Inject]
        private IncreaseStageProgressController(
            IControllerFactory controllerFactory,
            IStageProgress stageProgress)
            : base(controllerFactory, stageProgress)
        {
        }

        protected override void ApplyProgress() => StageProgress.Increase();
    }
}