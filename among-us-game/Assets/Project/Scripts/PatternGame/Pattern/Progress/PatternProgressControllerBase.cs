using Playtika.Controllers;

namespace PatternGame
{
    public abstract class PatternProgressControllerBase : ControllerBase
    {
        protected IPatternProgress PatternProgress { get;  }

        protected PatternProgressControllerBase(
            IControllerFactory controllerFactory,
            IPatternProgress patternProgress)
            : base(controllerFactory)
            => PatternProgress = patternProgress;

        protected override void OnStart() => ApplyProgress();
        protected abstract void ApplyProgress();
    }
}