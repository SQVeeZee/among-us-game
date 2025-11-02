using UnityEngine;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class BoardFactory
    {
        private readonly BoardAssetsConfig _config;

        [Inject]
        private BoardFactory(
            BoardAssetsConfig config)
        {
            _config = config;
        }

        public BoardView CreateBoard(Transform root)
        {
            var prefab = _config.BoardViewPrefab;
            return Object.Instantiate(prefab, root);
        }
    }
}