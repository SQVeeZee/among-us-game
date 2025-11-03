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

        private UniTaskCompletionSource<LevelResult> _levelCompletionSource;

        [Inject]
        private LevelController(
            IControllerFactory controllerFactory,
            LevelModel levelModel,
            IStageProgress stageProgress)
            : base(controllerFactory)
        {
            _stageProgress = stageProgress;
            _levelModel = levelModel;
        }

        protected override void OnStart()
        {
            _levelModel.ApplyCurrentLevel(Args.Stages);
            Execute<BoardViewController>();
            Execute<GridViewController, int>(Args.TilesCount);
            _levelCompletionSource = new UniTaskCompletionSource<LevelResult>();
        }

        protected override void OnStop() => TrySetResult(LevelResult.None);

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            ExecuteStageAsync(cancellationToken).Forget();
            var result = await _levelCompletionSource.Task.AttachExternalCancellation(cancellationToken);
            Complete(result);
        }

        //TODO: gameplay panel

        private async UniTaskVoid ExecuteStageAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var currentStage = _stageProgress.CurrentStage;
                var result = await ExecuteStageAndWaitForResultAsync(currentStage, cancellationToken);
                switch (result)
                {
                    case StageResult.Completed:
                        if (_levelModel.IsLastStage(currentStage))
                        {
                            Execute<ResetStageProgressController>();
                            TrySetResult(LevelResult.Completed);
                            return;
                        }
                        Execute<IncreaseStageProgressController>();
                        continue;
                    case StageResult.Failed:
                        Execute<ResetStageProgressController>();
                        continue;
                    case StageResult.None:
                        TrySetResult(LevelResult.None);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async UniTask<StageResult> ExecuteStageAndWaitForResultAsync(int currentStage, CancellationToken cancellationToken)
        {
            var stage = _levelModel.GetStageData(currentStage);
            var args = new StageArgs(Args.TilesCount, stage.PatternData);
            return await ExecuteAndWaitResultAsync<StageController, StageArgs, StageResult>(args, cancellationToken);
        }

        private void TrySetResult(LevelResult levelResult) => _levelCompletionSource?.TrySetResult(levelResult);
    }
}