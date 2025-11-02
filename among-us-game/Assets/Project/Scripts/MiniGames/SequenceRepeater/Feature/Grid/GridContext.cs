using System.Collections.Generic;

namespace MiniGames.SequenceRepeater
{
    public class GridContext
    {
        private readonly List<GridItemPair> _items = new ();
        private Dictionary<int, GridItemPair> _gridItems = new ();

        public IReadOnlyList<GridItemPair> Items => _items;

        public bool TryGetItem(int id, out GridItemPair item) => _gridItems.TryGetValue(id, out item);

        public void Add(GridItemPair item)
        {
            _items.Add(item);
            _gridItems.Add(item.Id, item);
        }

        public void Clean() => _gridItems.Clear();
    }
}