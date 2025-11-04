using UnityEngine;
using VContainer;

namespace PatternGame
{
    public class PanelFactory : MonoFactoryBase
    {
        private readonly PanelsConfig _config;
        private readonly Transform _root;

        [Inject]
        private PanelFactory(
            PanelsConfig config,
            [Key(UIConstants.PanelRoot)] Transform root)
        {
            _config = config;
            _root = root;
        }

        public GameplayPanel CreatePanel() => InstantiatePrefab(_config.GameplayPanel, _root);
        public void DestroyPanel(GameplayPanel gameplayPanel) => Destroy(gameplayPanel);
    }
}