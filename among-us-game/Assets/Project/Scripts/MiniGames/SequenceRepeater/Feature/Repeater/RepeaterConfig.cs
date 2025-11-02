using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [CreateAssetMenu(menuName = "Configs/Repeater/Repeater config", fileName = "repeater_config", order = 0)]
    public class RepeaterConfig : ScriptableObject
    {
        [SerializeField]
        private float _highlightDelay = 0.1f;

        public float HighlightDelay => _highlightDelay;
    }
}