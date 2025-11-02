using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [CreateAssetMenu(menuName = "Configs/Level/Levels Config", fileName = "levels_config", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private LevelData[] _levelData;

        public int LevelsCount => _levelData.Length;
        public LevelData GetLevelDataByIndex(int index) => _levelData[index];
    }
}