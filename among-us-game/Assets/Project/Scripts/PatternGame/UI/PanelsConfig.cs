using UnityEngine;

namespace PatternGame
{
    [CreateAssetMenu(menuName = "Configs/Game/UI panel config", fileName = "game_panel_config", order = 0)]
    public class PanelsConfig : ScriptableObject
    {
        [SerializeField]
        private GameplayPanel _gameplayPanel;

        public GameplayPanel GameplayPanel => _gameplayPanel;
    }
}