using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class LevelViewController : ControllerBase
    {
        [Inject]
        private LevelViewController(IControllerFactory controllerFactory) : base(controllerFactory)
        {
        }

        protected override void OnStart() => InitializeViews();

        private void InitializeViews()
        {
            Execute<BoardViewController>();
            Execute<GridViewController>();
        }
    }
}