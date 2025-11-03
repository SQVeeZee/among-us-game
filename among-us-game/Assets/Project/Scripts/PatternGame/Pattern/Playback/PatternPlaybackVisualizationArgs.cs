using System.Collections.Generic;

namespace PatternGame
{
    public readonly struct PatternPlaybackVisualizationArgs
    {
        public IReadOnlyList<int> Ids { get; }

        public PatternPlaybackVisualizationArgs(int[] ids) => Ids = ids;
    }
}