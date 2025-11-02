using UnityEngine;

namespace MiniGames.SequenceRepeater
{
    public class BoardContainer : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;

        public Transform Root => _root;
    }
}