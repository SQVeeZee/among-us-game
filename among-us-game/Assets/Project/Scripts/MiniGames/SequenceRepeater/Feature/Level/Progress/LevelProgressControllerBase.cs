using Playtika.Controllers;

namespace MiniGames.SequenceRepeater
{
    public abstract class LevelProgressControllerBase : ControllerBase
    {
        protected LevelModel LevelModel { get; private set; }

        protected LevelProgressControllerBase(
            IControllerFactory controllerFactory,
            LevelModel levelModel) : base(controllerFactory)
        {
            LevelModel = levelModel;
        }

        protected override void OnStart() => ApplyProgress();
        protected abstract void ApplyProgress();
    }
}