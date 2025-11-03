using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ResetStageProgressController : StageProgressControllerBase
    {
        [Inject]
        private ResetStageProgressController(
            IControllerFactory controllerFactory,
            IStageProgress stageProgress)
            : base(controllerFactory, stageProgress)
        {
        }

        protected override void ApplyProgress() => StageProgress.Reset();
    }
}