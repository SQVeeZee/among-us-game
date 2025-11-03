using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class PatternPlaybackController : ControllerWithResultBase<EmptyControllerArg, EmptyControllerResult>
    {
        private readonly PatternModel _patternModel;

        [Inject]
        private PatternPlaybackController(
            IControllerFactory controllerFactory,
            PatternModel patternModel)
            : base(controllerFactory)
            => _patternModel = patternModel;

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var sequence = _patternModel.GetSequence();
            var args = new PatternPlaybackVisualizationArgs(sequence);
            await ExecuteAndWaitResultAsync<PatternPlaybackVisualizationController, PatternPlaybackVisualizationArgs>(args, cancellationToken);
            Complete(new EmptyControllerResult());
        }
    }
}