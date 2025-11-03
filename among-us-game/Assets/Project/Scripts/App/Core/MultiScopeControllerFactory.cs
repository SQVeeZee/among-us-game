using Playtika.Controllers;
using VContainer;

namespace App
{
    public class MultiScopeControllerFactory : IControllerFactory
    {
        private readonly IObjectResolver _app;
        private IObjectResolver _module;

        [Inject]
        public MultiScopeControllerFactory(IObjectResolver app) => _app = app;

        public void SetModuleContainer(IObjectResolver module) => _module = module;
        public void ClearModuleContainer() => _module = null;

        public IController Create<T>() where T : class, IController
        {
            if (_module != null && _module.TryResolve(out T controller))
            {
                return controller;
            }

            return _app.Resolve<T>();
        }
    }
}