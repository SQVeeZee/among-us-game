using App;
using Playtika.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MiniGames.SequenceRepeater
{
    public class ModuleLifetimeScope : LifetimeScope
    {
        [Header("levels")]
        [SerializeField]
        private LevelConfig _levelConfig;

        [Header("board")]
        [SerializeField]
        private BoardContainer _boardContainer;
        [SerializeField]
        private BoardAssetsConfig _boardAssetsConfig;
        [SerializeField]
        private GridAssetsConfig _gridAssetsConfig;

        [Header("steps")]
        [SerializeField]
        private HighlightConfig _highlightConfig;
        [SerializeField]
        private RepeaterConfig _repeaterConfig;


        protected override void Configure(IContainerBuilder builder)
        {
            RegisterModuleCore(builder);

            builder.Register<IControllerFactory, ControllerFactory>(Lifetime.Transient);
            builder.Register<GameLoopController>(Lifetime.Transient);

            RegisterLevel(builder);
            RegisterBoard(builder);
            RegisterSequencePattern(builder);

            RegisterHighlight(builder);
        }

        private static void RegisterModuleCore(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ModuleBootstrap>();
            builder.Register<LevelService>(Lifetime.Singleton);
            builder.Register<VisualizationService>(Lifetime.Singleton);
            builder.Register<ModuleRootController>(Lifetime.Singleton);
        }

        private void RegisterBoard(IContainerBuilder builder)
        {
            builder.Register<BoardViewController>(Lifetime.Singleton);
            builder.Register<BoardFactory>(Lifetime.Singleton);
            builder.Register<BoardContext>(Lifetime.Singleton);
            builder.RegisterInstance(_boardContainer);
            builder.RegisterInstance(_boardAssetsConfig);

            builder.Register<GridViewController>(Lifetime.Singleton);
            builder.Register<GridModel>(Lifetime.Scoped);
            builder.Register<GridFactory>(Lifetime.Singleton);
            builder.Register<GridContext>(Lifetime.Singleton);
            builder.RegisterInstance(_gridAssetsConfig);
        }

        private void RegisterSequencePattern(IContainerBuilder builder)
        {
            builder.Register<PatternPlaybackController>(Lifetime.Transient);
            builder.Register<RepeaterVisualizationController>(Lifetime.Transient);
            builder.Register<SequenceGenerationController>(Lifetime.Transient);
            builder.RegisterInstance(_repeaterConfig);

            builder.Register<LevelSequenceModel>(Lifetime.Scoped);

            builder.Register<IncreaseSequenceProgressController>(Lifetime.Transient);
            builder.Register<ResetSequenceProgressController>(Lifetime.Transient);

            builder.Register<SelectSequenceController>(Lifetime.Transient);
            builder.Register<ValidationController>(Lifetime.Singleton);
            builder.Register<CorrectSequenceController>(Lifetime.Transient);
        }

        private void RegisterHighlight(IContainerBuilder builder)
        {
            builder.Register<HighlightVisualizationController>(Lifetime.Transient);
            builder.Register<HighlightVisualization>(Lifetime.Transient);
            builder.RegisterInstance(_highlightConfig);
        }

        private void RegisterLevel(IContainerBuilder builder)
        {
            builder.Register<LevelController>(Lifetime.Transient);
            builder.Register<LevelViewController>(Lifetime.Transient);

            builder.Register<LevelProgressIncreaseController>(Lifetime.Transient);
            builder.Register<LevelProgressResetController>(Lifetime.Transient);

            builder.Register<LevelModel>(Lifetime.Scoped);
            builder.RegisterInstance(_levelConfig);
        }
    }
}