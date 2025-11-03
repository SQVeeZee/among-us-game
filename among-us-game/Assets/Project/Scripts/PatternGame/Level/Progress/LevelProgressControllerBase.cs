using App;
using Playtika.Controllers;

namespace PatternGame
{
    public abstract class LevelProgressControllerBase : ControllerBase
    {
        protected ILevelProgress LevelProgress { get; private set; }

        protected LevelProgressControllerBase(
            IControllerFactory controllerFactory,
            ILevelProgress levelProgress)
            : base(controllerFactory)
            => LevelProgress = levelProgress;

        protected override void OnStart() => ApplyProgress();
        protected abstract void ApplyProgress();
    }
}