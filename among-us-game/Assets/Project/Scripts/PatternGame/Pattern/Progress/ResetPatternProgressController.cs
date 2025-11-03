using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ResetPatternProgressController : PatternProgressControllerBase
    {
        [Inject]
        private ResetPatternProgressController(
            IControllerFactory controllerFactory,
            IPatternProgress patternProgress)
            : base(controllerFactory, patternProgress)
        {
        }

        protected override void ApplyProgress() => PatternProgress.Reset();
    }
}