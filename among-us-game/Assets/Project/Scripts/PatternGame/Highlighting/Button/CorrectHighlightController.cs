using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class CorrectHighlightController : HighlightButtonControllerBase
    {
        [Inject]
        private CorrectHighlightController(
            IControllerFactory controllerFactory,
            IGridContext gridContext,
            [Key(HighlightType.Correct)] HighlightConfig config)
            : base(controllerFactory, gridContext, config)
        {

        }
    }
}