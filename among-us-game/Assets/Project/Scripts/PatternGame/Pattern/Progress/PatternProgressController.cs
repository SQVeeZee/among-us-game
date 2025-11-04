using System;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class PatternProgressController : ControllerWithResultBase<ClickResult, PatternResult>
    {
        private readonly IPatternProgress _patternProgress;

        [Inject]
        private PatternProgressController(
            IControllerFactory controllerFactory,
            IPatternProgress patternProgress)
            : base(controllerFactory)
            => _patternProgress = patternProgress;

        protected override void OnStart()
        {
            var clickResult = Args;
            switch (clickResult)
            {
                case ClickResult.Correct:
                    if (_patternProgress.IsLastStep())
                    {
                        Execute<ResetPatternProgressController>();
                        Complete(PatternResult.Completed);
                        return;
                    }
                    Complete(PatternResult.Continue);
                    Execute<IncreasePatternProgressController>();
                    break;
                case ClickResult.Wrong:
                    Execute<ResetPatternProgressController>();
                    Complete(PatternResult.Failed);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}