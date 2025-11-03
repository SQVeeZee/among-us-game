using UnityEngine;

namespace PatternGame
{
    [CreateAssetMenu(menuName = "Configs/Repeater/Pattern Playback config", fileName = "pattern_playback_config", order = 0)]
    public class PatternPlaybackConfig : ScriptableObject
    {
        [SerializeField]
        private float _highlightDelay = 0.1f;

        public float HighlightDelay => _highlightDelay;
    }
}