using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PatternGame
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _exitButton;
        [SerializeField]
        private Transform _root;

        [Header("info")]
        [SerializeField]
        private TextMeshProUGUI _level;
        [SerializeField]
        private TextMeshProUGUI _stage;

        private Action<PanelResult> _exitCallback;

        public Transform ContentRoot => _root;

        public void UpdateLevelInfo(int levelIndex, (int StageIndex, int StageCount) stageInfo)
        {
            _level.text = $"level_{levelIndex}";
            _stage.text = $"stage: {stageInfo.StageIndex}/{stageInfo.StageCount}";
        }

        public void Subscribe(Action<PanelResult> exitCallback)
        {
            _exitCallback = exitCallback;
            _exitButton.onClick.AddListener(HandleExitButtonClick);
        }

        public void Unsubscribe()
        {
            _exitButton.onClick.RemoveListener(HandleExitButtonClick);
            _exitCallback = null;
        }

        private void HandleExitButtonClick() => _exitCallback?.Invoke(PanelResult.Exit);
    }
}