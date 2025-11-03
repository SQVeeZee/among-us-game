using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class PatternController : ControllerWithResultBase<PatternArgs, PatternResult>
    {
        private readonly PatternModel _patternModel;

        [Inject]
        private PatternController(
            IControllerFactory controllerFactory,
            PatternModel patternModel)
            : base(controllerFactory)
            => _patternModel = patternModel;

        protected override void OnStart() => _patternModel.GeneratePattern(Args.Tiles, Args.Length);
        protected override void OnStop() => _patternModel.Reset();

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<PatternPlaybackController, EmptyControllerArg, EmptyControllerResult>(new EmptyControllerArg(), cancellationToken);
            var result = await ExecuteAndWaitResultAsync<PatternResolveController, EmptyControllerArg, PatternResult>(new EmptyControllerArg(), cancellationToken);
            Complete(result);
        }
    }
}