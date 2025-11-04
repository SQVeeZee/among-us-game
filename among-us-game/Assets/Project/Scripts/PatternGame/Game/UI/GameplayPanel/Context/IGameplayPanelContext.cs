namespace PatternGame
{
    public interface IGameplayPanelContext
    {
        GameplayPanel GameplayPanel { get; }
        void Bind(GameplayPanel panel);
        void UnBind();
    }
}