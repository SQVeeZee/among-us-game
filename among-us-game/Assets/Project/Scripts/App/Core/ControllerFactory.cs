using Playtika.Controllers;
using VContainer;

namespace App
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IObjectResolver _container;

        [Inject]
        public ControllerFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IController Create<T>() where T : class, IController
        {
            var controller = _container.Resolve<T>();
            return controller;
        }
    }
}