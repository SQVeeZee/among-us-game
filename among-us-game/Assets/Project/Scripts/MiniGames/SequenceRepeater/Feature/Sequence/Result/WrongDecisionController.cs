using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class WrongHighlightController : HighlightButtonController
    {
        [Inject]
        private WrongHighlightController(
            IControllerFactory controllerFactory,
            GridContext gridContext,
            [Key(HighlightType.Wrong)] HighlightConfig config)
            : base(controllerFactory, gridContext, config)
        {

        }
    }
}