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

        [Header("UI")]
        [SerializeField]
        private Transform _panelRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCore(builder);
            RegisterModule(builder);
            RegisterManager(builder);
        }

        private void RegisterCore(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            builder.Register<BootstrapRootController>(Lifetime.Singleton);
            builder.Register<BootstrapController>(Lifetime.Singleton);

            builder.Register<MultiScopeControllerFactory>(Lifetime.Singleton)
                .As<IControllerFactory>()
                .AsSelf();
        }

        private void RegisterManager(IContainerBuilder builder)
        {
            builder.Register<ProgressManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterModule(IContainerBuilder builder)
        {
            builder.Register<ModuleRunnerController>(Lifetime.Transient);
            builder.RegisterInstance(_moduleLifetimeScope);
            builder.Register<ModuleFactory>(Lifetime.Singleton);
            builder.RegisterInstance(gameObject.transform).Keyed(AppConstants.ModuleRoot);
            builder.RegisterInstance(_panelRoot).Keyed(UIConstants.PanelRoot);
        }
    }
}