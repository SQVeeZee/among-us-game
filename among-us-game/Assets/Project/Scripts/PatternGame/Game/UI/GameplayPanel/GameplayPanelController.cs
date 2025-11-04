using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class GameplayPanelController : ControllerWithResultBase<GameplayPanelArgs, GameplayPanelResult>
    {
        private readonly IGameplayPanelContext _gameplayPanelContext;
        private readonly PanelFactory _panelFactory;

        [Inject]
        private GameplayPanelController(
            IControllerFactory controllerFactory,
            PanelFactory panelFactory,
            IGameplayPanelContext gameplayPanelContext)
            : base(controllerFactory)
        {
            _panelFactory = panelFactory;
            _gameplayPanelContext = gameplayPanelContext;
        }

        protected override void OnStart()
        {
            var panel = _panelFactory.CreatePanel();
            _gameplayPanelContext.Bind(panel);
            ExecutePanelInfo(panel);
        }

        protected override void OnStop()
        {
            if (_gameplayPanelContext.GameplayPanel != null)
            {
                _panelFactory.DestroyPanel(_gameplayPanelContext.GameplayPanel);
                _gameplayPanelContext.UnBind();
            }
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteGameplayPanelAsync(cancellationToken);
            Complete(result);
        }

        private void ExecutePanelInfo(GameplayPanel panel)
        {
            var args = new GameplayPanelInfoArgs(panel, Args.StageCount);
            Execute<GameplayPanelInfoController, GameplayPanelInfoArgs>(args);
        }

        private async UniTask<GameplayPanelResult> ExecuteGameplayPanelAsync(CancellationToken cancellationToken)
        {
            var panel = _gameplayPanelContext.GameplayPanel;
            return await ExecuteAndWaitResultAsync<GameplayPanelInteractionController, GameplayPanel, GameplayPanelResult>(panel, cancellationToken);
        }
    }
}