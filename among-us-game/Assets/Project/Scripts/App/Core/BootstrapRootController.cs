using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace App
{
    public class BootstrapRootController : RootController
    {

        [Inject]
        private BootstrapRootController(
            IControllerFactory controllerFactory) : base(controllerFactory)
        {
        }

        protected override void OnStart()
        {
            RunSequenceRepeaterAsync(CancellationToken).Forget();
            base.OnStart();
        }

        private async UniTask RunSequenceRepeaterAsync(CancellationToken cancellationToken)
        {
        }
    }
}