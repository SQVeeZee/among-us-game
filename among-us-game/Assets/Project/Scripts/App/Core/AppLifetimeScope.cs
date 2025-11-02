using Playtika.Controllers;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class AppLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();

            builder.Register<BootstrapRootController>(Lifetime.Singleton);

            builder.Register<IControllerFactory, ControllerFactory>(Lifetime.Scoped);
        }
    }
}
