using Playtika.Controllers;

namespace PatternGame
{
    public abstract class StageProgressControllerBase : ControllerBase
    {
        protected IStageProgress StageProgress { get; }

        protected StageProgressControllerBase(
            IControllerFactory controllerFactory,
            IStageProgress levelProgress)
            : base(controllerFactory)
            => StageProgress = levelProgress;

        protected override void OnStart() => ApplyProgress();

        protected abstract void ApplyProgress();
    }
}