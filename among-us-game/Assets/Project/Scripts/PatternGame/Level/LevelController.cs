using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class LevelController : ControllerWithResultBase<LevelArgs, LevelResult>
    {
        private readonly LevelModel _levelModel;
        private readonly IStageProgress _stageProgress;

        [Inject]
        private LevelController(
            IControllerFactory controllerFactory,
            LevelModel levelModel,
            IStageProgress stageProgress)
            : base(controllerFactory)
        {
            _levelModel = levelModel;
            _stageProgress = stageProgress;
        }

        protected override void OnStart()
        {
            _levelModel.ApplyCurrentLevel(Args.Stages);

            Execute<BoardViewController>();
            Execute<GridViewController, int>(Args.TilesCount);
        }

        protected override void OnStop() => _stageProgress.Reset();

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var result = await ExecuteStagesAsync(cancellationToken);
            Complete(result);
        }

        private async UniTask<LevelResult> ExecuteStagesAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var currentStage = _stageProgress.CurrentStage;
                var stage = _levelModel.GetStageData(currentStage);
                var args = new StageArgs(Args.TilesCount, stage.PatternData);

                var result = await ExecuteAndWaitResultAsync<StageController, StageArgs, StageResult>(args, cancellationToken);
                switch (result)
                {
                    case StageResult.Completed:
                    {
                        if (_levelModel.IsLastStage(currentStage))
                        {
                            Execute<ResetStageProgressController>();
                            return LevelResult.Completed;
                        }

                        Execute<IncreaseStageProgressController>();
                        continue;
                    }
                    case StageResult.Failed:
                    {
                        Execute<ResetStageProgressController>();
                        continue;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(result), result, null);
                    }
                }
            }
        }
    }
}
