using System;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class SequenceProgressController: ControllerWithResultBase<ValidationResult, PatternProgressResult>
    {
        private readonly LevelSequenceModel _levelSequenceModel;

        [Inject]
        private SequenceProgressController(
            IControllerFactory controllerFactory,
            LevelSequenceModel levelSequenceModel)
            : base(controllerFactory)
            => _levelSequenceModel = levelSequenceModel;

        protected override void OnStart()
        {
            var result = GetPatternProgressResult(Args);
            ApplyProgress(result);
            Complete(result);
        }

        private PatternProgressResult GetPatternProgressResult(ValidationResult clickResult)
        {
            switch (clickResult)
            {
                case ValidationResult.Correct:
                    return _levelSequenceModel.IsLastStep()
                        ? PatternProgressResult.Success
                        : PatternProgressResult.Continue;
                case ValidationResult.Wrong:
                    return PatternProgressResult.Fail;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ApplyProgress(PatternProgressResult result)
        {
            switch (result)
            {
                case PatternProgressResult.Continue:
                    Execute<IncreaseSequenceProgressController>();
                    break;
                case PatternProgressResult.Success:
                case PatternProgressResult.Fail:
                    Execute<ResetSequenceProgressController>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}