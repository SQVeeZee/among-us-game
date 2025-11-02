using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    public class HighlightVisualization
    {
        public async UniTask Blink(IHighlighted highlighted, HighlightConfig config, CancellationToken cancellationToken)
        {
            var startColor = config.StartColor;
            var endColor = config.EndColor;
            var duration = config.Duration;
            await Blink(highlighted, startColor, endColor, duration, cancellationToken);
        }

        private static async UniTask Blink(IHighlighted highlighted, Color startColor, Color endColor, float duration, CancellationToken cancellationToken)
        {
            await ChangeColor(highlighted, startColor, endColor, duration, cancellationToken);
            await ChangeColor(highlighted, endColor, startColor, duration, cancellationToken);
        }

        private static async UniTask ChangeColor(IHighlighted highlighted, Color startColor, Color endColor, float duration, CancellationToken cancellationToken)
        {
            if (duration <= 0f)
            {
                highlighted.UpdateColor(endColor);
                return;
            }

            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                var t = Mathf.Clamp01(time / duration);

                var currentColor = Color32.Lerp(startColor, endColor, t);
                highlighted.UpdateColor(currentColor);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: cancellationToken);
            }

            highlighted.UpdateColor(endColor);
        }
    }
}