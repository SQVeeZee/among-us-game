using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class HighlightButtonController : ControllerWithResultBase<int, bool>
    {
        private readonly GridContext _gridContext;
        private IHighlighted _buttonView;

        [Inject]
        private HighlightButtonController(
            IControllerFactory controllerFactory,
            GridContext gridContext) : base(controllerFactory)
            => _gridContext = gridContext;

        protected override void OnStart()
        {
            if (_gridContext.TryGetItem(Args, out var item))
            {
                _buttonView = item.ButtonView;
            }
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
            => await ExecuteAndWaitResultAsync<HighlightVisualizationController, IHighlighted, EmptyControllerResult>(_buttonView, cancellationToken);
    }
}