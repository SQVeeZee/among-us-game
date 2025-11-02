using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class CorrectHighlightController : HighlightButtonController
    {
        [Inject]
        private CorrectHighlightController(
            IControllerFactory controllerFactory,
            GridContext gridContext,
            [Key(HighlightType.Correct)] HighlightConfig config)
            : base(controllerFactory, gridContext, config)
        {

        }
    }
}