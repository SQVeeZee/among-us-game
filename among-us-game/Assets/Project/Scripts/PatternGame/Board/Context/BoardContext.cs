using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace PatternGame
{
    [UsedImplicitly]
    public class BoardContext : IBoardContext, IGridContext
    {
        private readonly List<GridItemPair> _items = new ();
        private readonly Dictionary<int, GridItemPair> _gridItems = new ();

        public BoardView BoardView { get; private set; }
        public IReadOnlyList<GridItemPair> Items => _items;

        void IBoardContext.Bind(BoardView view) => BoardView = view;
        void IBoardContext.UnBind() => BoardView = null;


        bool IGridContext.TryGetItem(int id, out GridItemPair item) => _gridItems.TryGetValue(id, out item);
        Transform IGridContext.TileRoot => BoardView.TileRoot;
        Transform IGridContext.ButtonRoot => BoardView.ButtonRoot;

        void IGridContext.Add(GridItemPair item)
        {
            _items.Add(item);
            _gridItems.Add(item.Id, item);
        }
        void IGridContext.Clean() => _gridItems.Clear();
    }
}