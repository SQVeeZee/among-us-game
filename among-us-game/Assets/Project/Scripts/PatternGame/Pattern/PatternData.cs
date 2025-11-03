using System;
using UnityEngine;

namespace PatternGame
{
    [Serializable]
    public struct PatternData
    {
        [SerializeField]
        private int _length;
        
        public int Length => _length;
    }
}