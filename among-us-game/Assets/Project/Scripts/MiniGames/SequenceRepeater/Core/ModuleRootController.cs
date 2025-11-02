using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class ModuleRootController : RootController
    {
        [Inject]
        private ModuleRootController(IControllerFactory controllerFactory) : base(controllerFactory)
        {

        }

        protected override void OnStart()
        {
            RunGameLoopAndWaitForResultAsync(CancellationToken).Forget();
            base.OnStart();
        }

        private async UniTask RunGameLoopAndWaitForResultAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<GameLoopController, GameLoopResult>(cancellationToken);
            switch (result)
            {
                case GameLoopResult.Success:
                    break;
                case GameLoopResult.Fail:
                    break;
            }
        }
    }
}