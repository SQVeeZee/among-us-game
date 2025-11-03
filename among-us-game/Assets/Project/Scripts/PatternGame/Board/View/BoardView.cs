using UnityEngine;

namespace PatternGame
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField]
        private Transform _tileRoot;
        [SerializeField]
        private Transform _buttonRoot;

        public Transform TileRoot => _tileRoot;
        public Transform ButtonRoot => _buttonRoot;
    }
}