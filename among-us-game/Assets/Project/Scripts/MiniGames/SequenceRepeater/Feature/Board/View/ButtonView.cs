using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames.SequenceRepeater
{
    public class ButtonView : MonoBehaviour, IHighlighted
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _image;

        private Action _action;

        public void Subscribe(Action action)
        {
            _action = action;
            _button.onClick.AddListener(ButtonClickHandle);
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(ButtonClickHandle);
        }

        public void UpdateColor(Color color) => _image.color = color;

        private void ButtonClickHandle()
        {
            _action?.Invoke();
        }
    }
}