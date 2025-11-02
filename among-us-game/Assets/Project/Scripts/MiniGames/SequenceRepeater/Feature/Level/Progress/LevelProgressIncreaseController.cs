using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelProgressIncreaseController : LevelProgressControllerBase
    {
        [Inject]
        private LevelProgressIncreaseController(IControllerFactory controllerFactory, LevelModel levelModel) : base(controllerFactory, levelModel)
        {
        }

        protected override void ApplyProgress() => LevelModel.Increase();
    }
}