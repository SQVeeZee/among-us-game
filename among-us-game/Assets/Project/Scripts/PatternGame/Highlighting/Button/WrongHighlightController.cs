using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class WrongHighlightController : HighlightButtonControllerBase
    {
        [Inject]
        private WrongHighlightController(
            IControllerFactory controllerFactory,
            IGridContext gridContext,
            [Key(HighlightType.Wrong)] HighlightConfig config)
            : base(controllerFactory, gridContext, config)
        {

        }
    }
}