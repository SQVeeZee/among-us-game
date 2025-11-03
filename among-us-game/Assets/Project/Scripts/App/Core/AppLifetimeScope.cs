using PatternGame;
using Playtika.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class AppLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ModuleLifetimeScope _moduleLifetimeScope;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCore(builder);
            RegisterModule(builder);
        }

        private void RegisterCore(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            builder.Register<BootstrapRootController>(Lifetime.Transient);
            builder.Register<BootstrapController>(Lifetime.Transient);
            builder.Register<MultiScopeControllerFactory>(Lifetime.Singleton)
                .As<IControllerFactory>()
                .AsSelf();
        }

        private void RegisterModule(IContainerBuilder builder)
        {
            builder.Register<ProgressManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ModuleRunnerController>(Lifetime.Transient);
            builder.RegisterInstance(_moduleLifetimeScope);
        }
    }
}