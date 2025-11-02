using System.Collections.Generic;

namespace MiniGames.SequenceRepeater
{
    public readonly struct RepeaterVisualizationArgs
    {
        public IReadOnlyList<int> Ids { get; }

        public RepeaterVisualizationArgs(int[] ids) => Ids = ids;
    }
}