using JetBrains.Annotations;

namespace PatternGame
{
    [UsedImplicitly]
    public class UIContext : IGameplayPanelContext
    {
        public GameplayPanel GameplayPanel { get; private set; }

        void IGameplayPanelContext.Bind(GameplayPanel panel) => GameplayPanel = panel;
        void IGameplayPanelContext.UnBind() => GameplayPanel = null;
    }
}