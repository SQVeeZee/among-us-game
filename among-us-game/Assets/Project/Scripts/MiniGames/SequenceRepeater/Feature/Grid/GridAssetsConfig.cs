using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [CreateAssetMenu(menuName = "Configs/Game/Grid Asset Config", fileName = "game_grid_asset_config", order = 0)]
    public class GridAssetsConfig : ScriptableObject
    {
        [SerializeField]
        private TileView _tileView;
        [SerializeField]
        private ButtonView _buttonView;

        public TileView TileView => _tileView;
        public ButtonView ButtonView => _buttonView;
    }
}