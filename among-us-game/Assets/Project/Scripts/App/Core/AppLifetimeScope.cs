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
        [Header("ui")]
        [SerializeField]
        private Transform _panelRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCore(builder);
            RegisterModule(builder);
        }

        private void RegisterCore(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            builder.Register<BootstrapRootController>(Lifetime.Singleton);
            builder.Register<BootstrapController>(Lifetime.Singleton);
            builder.Register<MultiScopeControllerFactory>(Lifetime.Singleton)
                .As<IControllerFactory>()
                .AsSelf();

            builder.Register<ModuleFactory>(Lifetime.Singleton);
            builder.RegisterInstance(gameObject.transform).Keyed("root");
            builder.RegisterInstance(_panelRoot).Keyed(UIConstants.PanelRoot);
        }

        private void RegisterModule(IContainerBuilder builder)
        {
            builder.Register<ProgressManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ModuleRunnerController>(Lifetime.Transient);
            builder.RegisterInstance(_moduleLifetimeScope);
        }
    }
}