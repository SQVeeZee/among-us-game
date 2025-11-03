using System;
using UnityEngine;

namespace PatternGame
{
    [Serializable]
    public class StageData
    {
        [SerializeField]
        private PatternData _patternData;

        public PatternData PatternData => _patternData;
    }
}