using UnityEngine;

namespace PatternGame
{
    public class BoardContainer : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;

        public Transform Root => _root;
    }
}