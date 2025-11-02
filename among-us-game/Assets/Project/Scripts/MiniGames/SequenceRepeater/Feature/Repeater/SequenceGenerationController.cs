using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class SequenceGenerationController : ControllerBase
    {
        private readonly LevelSequenceModel _sequencePatternModel;
        private readonly LevelModel _levelModel;

        [Inject]
        private protected SequenceGenerationController(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequencePatternModel,
            LevelModel levelModel)
            : base(controllerFactory)
        {
            _sequencePatternModel = sequencePatternModel;
            _levelModel = levelModel;
        }

        protected override void OnStart()
        {
            var tilesCount = _levelModel.GetTilesCount();
            var levelSequence = _levelModel.GetLevelSequence();
            _sequencePatternModel.GenerateSequence(tilesCount, levelSequence.Length);
        }
    }
}