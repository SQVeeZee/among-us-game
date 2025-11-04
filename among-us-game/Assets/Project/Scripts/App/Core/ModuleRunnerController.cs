using System.Threading;
using Cysharp.Threading.Tasks;
using PatternGame;
using Playtika.Controllers;
using VContainer;

namespace App
{
    public class ModuleRunnerController : ControllerWithResultBase<EmptyControllerArg, ModuleResult>
    {
        private readonly ModuleFactory _moduleFactory;
        private readonly MultiScopeControllerFactory _factory;
        private ModuleLifetimeScope _module;

        [Inject]
        private ModuleRunnerController(
            IControllerFactory factory,
            ModuleFactory moduleFactory,
            MultiScopeControllerFactory multiFactory)
            : base(factory)
        {
            _moduleFactory = moduleFactory;
            _factory = multiFactory;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<ModuleController, EmptyControllerArg, ModuleResult>(new EmptyControllerArg(), cancellationToken);
            Complete(result);
        }

        protected override void OnStart()
        {
            _module = _moduleFactory.Create();
            _module.Build();
            _factory.SetModuleContainer(_module.Container);
        }

        protected override void OnStop()
        {
            _module.Dispose();
            _moduleFactory.Dispose(_module);
            _factory.ClearModuleContainer();
        }
    }
}