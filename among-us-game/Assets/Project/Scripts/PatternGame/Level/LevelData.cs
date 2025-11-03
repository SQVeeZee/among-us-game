using UnityEngine;

namespace PatternGame
{
    [CreateAssetMenu(menuName = "Configs/Level/Data Config", fileName = "level_data_config", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _tilesCount;
        [SerializeField]
        private StageData[] _stages;

        public int TilesCount => _tilesCount;
        public StageData[] Stages => _stages;
    }
}