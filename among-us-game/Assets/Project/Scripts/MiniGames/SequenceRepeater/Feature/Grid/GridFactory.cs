using UnityEngine;
using VContainer;

namespace MiniGames.SequenceRepeater
{
    public class GridFactory : MonoFactoryBase
    {
        private readonly GridAssetsConfig _config;

        [Inject]
        private GridFactory(GridAssetsConfig config) => _config = config;

        public ButtonView CreateButtonView(Transform root) => InstantiatePrefab(_config.ButtonView, root);
        public TileView CreateTileView(Transform root) => InstantiatePrefab(_config.TileView, root);
    }
}