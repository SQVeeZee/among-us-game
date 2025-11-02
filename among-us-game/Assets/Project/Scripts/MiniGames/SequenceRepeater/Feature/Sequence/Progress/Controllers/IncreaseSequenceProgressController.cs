using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class IncreaseSequenceProgressController : SequenceProgressControllerBase
    {
        [Inject]
        private IncreaseSequenceProgressController(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequencePatternModel)
            : base(controllerFactory, sequencePatternModel)
        {
        }

        protected override void ApplyProgress() => SequencePatternModel.Increase();
    }
}