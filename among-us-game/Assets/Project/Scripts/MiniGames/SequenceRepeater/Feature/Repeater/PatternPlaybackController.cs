using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class PatternPlaybackController : ControllerWithResultBase
    {
        private readonly LevelSequenceModel _sequenceModel;

        [Inject]
        private PatternPlaybackController(
            IControllerFactory controllerFactory,
            LevelSequenceModel sequenceModel)
            : base(controllerFactory)
            => _sequenceModel = sequenceModel;

        protected override void OnStart() => Execute<SequenceGenerationController>();

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var sequence = _sequenceModel.GetSequence();
            var args = new RepeaterVisualizationArgs(sequence);
            await ExecuteAndWaitResultAsync<RepeaterVisualizationController, RepeaterVisualizationArgs>(args, cancellationToken);
            Complete();
        }
    }
}