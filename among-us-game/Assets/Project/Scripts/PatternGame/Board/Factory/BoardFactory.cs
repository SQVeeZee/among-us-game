using UnityEngine;
using VContainer;

namespace PatternGame
{
    public class BoardFactory : MonoFactoryBase
    {
        private readonly BoardAssetsConfig _config;

        [Inject]
        private BoardFactory(BoardAssetsConfig config) => _config = config;

        public BoardView CreateBoard(Transform root) => InstantiatePrefab(_config.BoardViewPrefab, root);
        public void Destroy(BoardView view) => base.Destroy(view);
    }
}