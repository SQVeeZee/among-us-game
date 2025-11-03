using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;
using VContainer;

namespace PatternGame
{
    public class PatternPlaybackVisualizationController : ControllerWithResultBase<PatternPlaybackVisualizationArgs, EmptyControllerResult>
    {
        private readonly IGridContext _gridContext;
        private readonly PatternPlaybackConfig _config;
        private readonly HighlightConfig _highlightConfig;

        [Inject]
        private PatternPlaybackVisualizationController(
            IControllerFactory controllerFactory,
            IGridContext gridContext,
            PatternPlaybackConfig patternPlaybackConfig,
            [Key(HighlightType.Repeater)] HighlightConfig highlightConfig)
            : base(controllerFactory)
        {
            _gridContext = gridContext;
            _config = patternPlaybackConfig;
            _highlightConfig = highlightConfig;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var highlights = ResolveHighlights(Args.Ids);
            var timeSpanDelay = TimeSpan.FromSeconds(_config.HighlightDelay);

            foreach (var id in highlights)
            {
                var args = new HighlightVisualizationArgs(id, _highlightConfig);
                await ExecuteAndWaitResultAsync<HighlightVisualizationController, HighlightVisualizationArgs, EmptyControllerResult>(args, cancellationToken);
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