using App;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class IncreaseLevelProgressController : LevelProgressControllerBase
    {
        [Inject]
        private IncreaseLevelProgressController(
            IControllerFactory controllerFactory,
            ILevelProgress levelProgress)
            : base(controllerFactory, levelProgress)
        {
        }

        protected override void ApplyProgress() => LevelProgress.Increase();
    }
}