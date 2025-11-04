using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace PatternGame
{
    [UsedImplicitly]
    public class HighlightVisualizer : IDisposable
    {
        private readonly Dictionary<IHighlighted, CancellationTokenSource> _tokens = new();

        void IDisposable.Dispose()
        {
            foreach (var (_, cts) in _tokens)
            {
                cts.Cancel();
                cts.Dispose();
            }
            _tokens.Clear();
        }

        public async UniTask BlinkAsync(IHighlighted highlighted, HighlightConfig config, CancellationToken externalToken)
        {
            if (_tokens.TryGetValue(highlighted, out var cancellationTokenSource))
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }

            var visualizationCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
            _tokens[highlighted] = visualizationCancellationToken;

            try
            {
                await BlinkInternalAsync(highlighted, config, visualizationCancellationToken.Token);
            }
            catch (OperationCanceledException)
            {

            }
            finally
            {
                if (_tokens.TryGetValue(highlighted, out var current) && current == visualizationCancellationToken)
                {
                    _tokens.Remove(highlighted);
                }

                visualizationCancellationToken.Dispose();
            }
        }

        private static async UniTask BlinkInternalAsync(IHighlighted highlighted, HighlightConfig config, CancellationToken cancellationToken)
        {
            var startColor = config.StartColor;
            var endColor = config.EndColor;
            var duration = config.Duration;

            await ChangeColorAsync(highlighted, startColor, endColor, duration, cancellationToken);
            await ChangeColorAsync(highlighted, endColor, startColor, duration, cancellationToken);
        }

        private static async UniTask ChangeColorAsync(IHighlighted highlighted, Color startColor, Color endColor, float duration, CancellationToken cancellationToken)
        {
            if (duration <= 0f)
            {
                highlighted.UpdateColor(endColor);
                return;
            }

            var time = 0f;
            while (time < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                time += Time.deltaTime;
                var t = Mathf.Clamp01(time / duration);
                var currentColor = Color.Lerp(startColor, endColor, t);

                highlighted.UpdateColor(currentColor);
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: cancellationToken);
            }

            highlighted.UpdateColor(endColor);
        }
    }
}
