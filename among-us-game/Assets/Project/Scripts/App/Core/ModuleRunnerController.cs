using System.Threading;
using Cysharp.Threading.Tasks;
using PatternGame;
using Playtika.Controllers;
using VContainer;

namespace App
{
    public class ModuleRunnerController : ControllerWithResultBase<EmptyControllerArg, ModuleResult>
    {
        private readonly ModuleLifetimeScope _moduleLifetimeScope;
        private readonly MultiScopeControllerFactory _factory;

        [Inject]
        private ModuleRunnerController(
            IControllerFactory factory,
            ModuleLifetimeScope moduleLifetimeScope,
            MultiScopeControllerFactory multiFactory)
            : base(factory)
        {
            _moduleLifetimeScope = moduleLifetimeScope;
            _factory = multiFactory;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteAndWaitResultAsync<ModuleController, EmptyControllerArg, ModuleResult>(new EmptyControllerArg(), cancellationToken);
            Complete(result);
        }

        protected override void OnStart()
        {
            _moduleLifetimeScope.Build();
            _factory.SetModuleContainer(_moduleLifetimeScope.Container);
        }

        protected override void OnStop() => _factory.ClearModuleContainer();
    }
}