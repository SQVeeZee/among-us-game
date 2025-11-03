using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class IncreasePatternProgressController : PatternProgressControllerBase
    {
        [Inject]
        private IncreasePatternProgressController(
            IControllerFactory controllerFactory,
            IPatternProgress patternProgress)
            : base(controllerFactory, patternProgress)
        {
        }

        protected override void ApplyProgress() => PatternProgress.Increase();
    }
}