using UnityEngine;

namespace PatternGame
{
    [CreateAssetMenu(menuName = "Configs/Game/Board Asset Config", fileName = "game_board_asset_config", order = 0)]
    public class BoardAssetsConfig : ScriptableObject
    {
        [SerializeField]
        private BoardView _boardViewPrefab;

        public BoardView BoardViewPrefab => _boardViewPrefab;
    }
}