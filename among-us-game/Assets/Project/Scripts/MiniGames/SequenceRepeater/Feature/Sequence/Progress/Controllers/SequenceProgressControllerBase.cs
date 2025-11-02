using Playtika.Controllers;

namespace MiniGames.SequenceRepeater
{
    public abstract class SequenceProgressControllerBase : ControllerBase
    {
        protected LevelSequenceModel SequencePatternModel { get; }

        protected SequenceProgressControllerBase(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequencePatternModel)
            : base(controllerFactory)
            => SequencePatternModel = sequencePatternModel;

        protected override void OnStart() => ApplyProgress();

        protected abstract void ApplyProgress();
    }
}