using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [CreateAssetMenu(menuName = "Configs/Level/Data Config", fileName = "level_data_config", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _tilesCount;

        [SerializeField]
        private LevelSequence[] _levelSequences;

        public LevelSequence[] LevelSequences => _levelSequences;
        public int TilesCount => _tilesCount;
    }
}