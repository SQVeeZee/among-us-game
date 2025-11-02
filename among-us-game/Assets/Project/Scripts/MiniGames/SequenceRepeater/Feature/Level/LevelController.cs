using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelController : ControllerWithResultBase<LevelData, LevelResultType>
    {
        private readonly LevelModel _levelModel;

        [Inject]
        private LevelController(
            IControllerFactory controllerFactory,
            LevelModel levelModel,
            LevelService levelService)
            : base(controllerFactory)
            => _levelModel = levelModel;

        protected override void OnStart()
        {
            _levelModel.ApplyCurrentLevel(Args);
            Execute<LevelViewController>();
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteAndWaitResultAsync<PatternPlaybackController>(cancellationToken);
                var result = await ExecuteAndWaitResultAsync<InputLoopController, PatternProgressResult>(cancellationToken);
                switch (result)
                {
                    case PatternProgressResult.Success:
                        if (!_levelModel.IsLastStep())
                        {
                            Execute<LevelProgressIncreaseController>();
                            break;
                        }
                        Execute<LevelProgressResetController>();
                        Complete(LevelResultType.Completed);
                        return;
                    case PatternProgressResult.Fail:
                        Execute<LevelProgressResetController>();
                        Complete(LevelResultType.Fail);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}