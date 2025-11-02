using System;
using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    [Serializable]
    public struct LevelSequence
    {
        [SerializeField]
        private int _length;

        public int Length => _length;
    }
}