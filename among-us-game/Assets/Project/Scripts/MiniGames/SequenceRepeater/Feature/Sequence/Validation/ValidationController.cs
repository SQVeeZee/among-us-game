using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class ClickValidationController : ControllerWithResultBase<ValidationArgs, ValidationResult>
    {
        private readonly LevelSequenceModel _sequencePatternModel;

        [Inject]
        private ClickValidationController(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequencePatternModel)
            : base(controllerFactory)
            => _sequencePatternModel = sequencePatternModel;

        protected override void OnStart()
        {
            var id = Args.Id;
            var correct = _sequencePatternModel.ValidateStep(id);
            Complete(correct ? ValidationResult.Correct : ValidationResult.Wrong);
        }
    }
}