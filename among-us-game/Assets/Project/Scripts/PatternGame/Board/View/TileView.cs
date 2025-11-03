using UnityEngine;
using UnityEngine.UI;

namespace PatternGame
{
    public class TileView : MonoBehaviour, IHighlighted
    {
        [SerializeField]
        private Image _image;

        public void UpdateColor(Color color) => _image.color = color;
    }
}