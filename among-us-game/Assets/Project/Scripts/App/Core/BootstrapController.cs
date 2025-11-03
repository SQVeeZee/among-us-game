using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PatternGame;
using Playtika.Controllers;
using VContainer;

namespace App
{
    public class BootstrapController : ControllerWithResultBase<EmptyControllerArg, BootstrapResult>
    {
        [Inject]
        private BootstrapController(IControllerFactory factory) : base(factory)
        {
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
            => await LoopModuleAsync(cancellationToken);

        private async UniTask LoopModuleAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await ExecuteAndWaitResultAsync<ModuleRunnerController, EmptyControllerArg, ModuleResult>(new EmptyControllerArg(), cancellationToken);
                switch (result)
                {
                    case ModuleResult.Completed:
                    case ModuleResult.Failed:
                    case ModuleResult.Exit:
                        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
                        break;
                    case ModuleResult.Unknown:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Complete(BootstrapResult.Completed);
        }
    }
}