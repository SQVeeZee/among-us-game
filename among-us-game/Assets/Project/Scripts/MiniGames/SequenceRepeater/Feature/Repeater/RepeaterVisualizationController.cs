using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class RepeaterVisualizationController : VisualizationControllerBase<RepeaterVisualizationArgs, EmptyControllerResult>
    {
        private readonly GridContext _gridContext;
        private readonly RepeaterConfig _repeaterConfig;

        [Inject]
        private RepeaterVisualizationController(
            IControllerFactory controllerFactory,
            VisualizationService visualizationService,
            GridContext gridContext,
            RepeaterConfig repeaterConfig)
            : base(controllerFactory, visualizationService)
        {
            _gridContext = gridContext;
            _repeaterConfig = repeaterConfig;
        }

        protected override async UniTask OnVisualizing(CancellationToken cancellationToken)
        {
            var highlights = ResolveHighlights(Args.Ids);
            var timeSpanDelay = TimeSpan.FromSeconds(_repeaterConfig.HighlightDelay);

            foreach (var highlighted in highlights)
            {
                await ExecuteAndWaitResultAsync<HighlightVisualizationController, IHighlighted, EmptyControllerResult>(highlighted, cancellationToken);
                await UniTask.Delay(timeSpanDelay, cancellationToken: cancellationToken);
            }

            Complete(new EmptyControllerResult());
        }

        private List<IHighlighted> ResolveHighlights(IReadOnlyList<int> ids)
        {
            var list = new List<IHighlighted>(ids.Count);
            foreach (var id in ids)
            {
                if (_gridContext.TryGetItem(id, out var item))
                {
                    list.Add(item.TileView);
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"[RepeaterVis] Missing UI for id={id}");
                }
            }
            return list;
        }
    }
}