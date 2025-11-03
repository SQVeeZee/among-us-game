using App;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ResetLevelProgressController : LevelProgressControllerBase
    {
        [Inject]
        private ResetLevelProgressController(
            IControllerFactory controllerFactory,
            ILevelProgress levelProgress)
            : base(controllerFactory, levelProgress)
        {
        }

        protected override void ApplyProgress() => LevelProgress.Reset();
    }
}