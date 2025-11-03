using System.Collections.Generic;
using UnityEngine;

namespace PatternGame
{
    public interface IGridContext
    {
        IReadOnlyList<GridItemPair> Items { get; }
        bool TryGetItem(int id, out GridItemPair item);

        Transform TileRoot { get; }
        Transform ButtonRoot { get; }

        void Add(GridItemPair item);
        void Clean();
    }
}