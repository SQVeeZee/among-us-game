using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class ClickValidationController : ControllerWithResultBase<ValidationArgs, ClickResult>
    {
        private readonly IPatternValidation _patternValidation;

        [Inject]
        private ClickValidationController(
            IControllerFactory controllerFactory,
            IPatternValidation patternValidation)
            : base(controllerFactory)
            => _patternValidation = patternValidation;

        protected override void OnStart()
        {
            var id = Args.Id;
            var correct = _patternValidation.ValidateStep(id);
            Complete(correct ? ClickResult.Correct : ClickResult.Wrong);
        }
    }
}