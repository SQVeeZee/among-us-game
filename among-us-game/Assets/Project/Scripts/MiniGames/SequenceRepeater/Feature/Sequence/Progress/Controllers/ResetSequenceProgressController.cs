using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class ResetSequenceProgressController : SequenceProgressControllerBase
    {
        [Inject]
        private ResetSequenceProgressController(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequencePatternModel)
            : base(controllerFactory, sequencePatternModel)
        {
        }

        protected override void ApplyProgress() => SequencePatternModel.Reset();
    }
}