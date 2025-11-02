using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelProgressResetController : LevelProgressControllerBase
    {
        [Inject]
        private LevelProgressResetController(IControllerFactory controllerFactory, LevelModel levelModel) : base(controllerFactory, levelModel)
        {
        }

        protected override void ApplyProgress() => LevelModel.Reset();
    }
}