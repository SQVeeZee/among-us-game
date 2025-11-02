using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [CreateAssetMenu(menuName = "Configs/Game/Highlight Config", fileName = "highlight_board_asset_config", order = 0)]
    public class HighlightConfig : ScriptableObject
    {
        [SerializeField]
        private Color _startColor;
        [SerializeField]
        private Color _endColor;
        [SerializeField]
        private float _duration;

        public Color StartColor => _startColor;
        public Color EndColor => _endColor;
        public float Duration => _duration;
    }
}