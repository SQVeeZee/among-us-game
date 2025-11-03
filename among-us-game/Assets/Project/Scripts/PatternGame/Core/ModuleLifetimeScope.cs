using App;
using Playtika.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PatternGame
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
        private HighlightConfig _repeaterHighlightConfig;
        [SerializeField]
        private HighlightConfig _correctHighlightConfig;
        [SerializeField]
        private HighlightConfig _wrongHighlightConfig;
        [SerializeField]
        private PatternPlaybackConfig _patternPlaybackConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCore(builder);

            RegisterLevel(builder);
            RegisterBoard(builder);
            RegisterStage(builder);
            RegisterPattern(builder);

            RegisterHighlight(builder);
        }

        private static void RegisterCore(IContainerBuilder builder)
        {
            builder.Register<ModuleController>(Lifetime.Transient).AsImplementedInterfaces().AsSelf();
        }

        private void RegisterLevel(IContainerBuilder builder)
        {
            builder.Register<LevelManager>(Lifetime.Scoped);

            //config
            builder.RegisterInstance(_levelConfig);

            //progress
            builder.Register<IncreaseLevelProgressController>(Lifetime.Transient);
            builder.Register<ResetLevelProgressController>(Lifetime.Transient);

            //
            builder.Register<LevelModel>(Lifetime.Scoped);
            builder.Register<LevelController>(Lifetime.Transient);
        }

        private void RegisterBoard(IContainerBuilder builder)
        {
            //context
            builder.Register<BoardContext>(Lifetime.Singleton).AsImplementedInterfaces();

            //factory
            builder.RegisterInstance(_boardAssetsConfig);
            builder.RegisterInstance(_boardContainer);
            builder.Register<BoardFactory>(Lifetime.Scoped);
            builder.Register<BoardViewController>(Lifetime.Transient);

            //grid
            builder.RegisterInstance(_gridAssetsConfig);
            builder.Register<GridFactory>(Lifetime.Scoped);
            builder.Register<GridViewController>(Lifetime.Transient);
        }

        private static void RegisterStage(IContainerBuilder builder)
        {
            //progress
            builder.Register<IncreaseStageProgressController>(Lifetime.Transient);
            builder.Register<ResetStageProgressController>(Lifetime.Transient);

            //
            builder.Register<StageController>(Lifetime.Transient);
        }

        private void RegisterPattern(IContainerBuilder builder)
        {
            //playback
            builder.RegisterInstance(_patternPlaybackConfig);
            builder.Register<PatternPlaybackController>(Lifetime.Transient);
            builder.Register<PatternPlaybackVisualizationController>(Lifetime.Transient);

            //progress
            builder.Register<IncreasePatternProgressController>(Lifetime.Transient);
            builder.Register<ResetPatternProgressController>(Lifetime.Transient);

            //validation
            builder.Register<ClickValidationController>(Lifetime.Transient);

            //
            builder.Register<PatternController>(Lifetime.Transient);
            builder.Register<PatternModel>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.Register<PatternResolveController>(Lifetime.Transient);
            builder.Register<PatternProgressController>(Lifetime.Transient);
        }

        private void RegisterHighlight(IContainerBuilder builder)
        {
            //button
            builder.Register<ButtonVisualizationController>(Lifetime.Transient);
            builder.Register<CorrectHighlightController>(Lifetime.Transient);
            builder.Register<WrongHighlightController>(Lifetime.Transient);

            //configs
            builder.RegisterInstance(_repeaterHighlightConfig).Keyed(HighlightType.Repeater);
            builder.RegisterInstance(_correctHighlightConfig).Keyed(HighlightType.Correct);
            builder.RegisterInstance(_wrongHighlightConfig).Keyed(HighlightType.Wrong);

            //
            builder.Register<HighlightVisualizationController>(Lifetime.Transient);
        }
    }
}