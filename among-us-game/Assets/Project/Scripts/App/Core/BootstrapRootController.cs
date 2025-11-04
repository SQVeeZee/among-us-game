using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace App
{
    public class BootstrapRootController : RootController
    {
        [Inject]
        private BootstrapRootController(IControllerFactory factory) : base(factory)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            RunFlow(CancellationToken).Forget();
        }

        private async UniTaskVoid RunFlow(CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<BootstrapController, BootstrapResult>(cancellationToken);
        }
    }
}