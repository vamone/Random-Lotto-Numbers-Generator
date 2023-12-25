public class UIConfig
{
    internal List<UIActionConfig> _actions = new List<UIActionConfig>();

    public UIConfig(Action<UIConfig> config)
    {
        config.Invoke(this);
    }

    public void Add(Action<UIActionConfig> action)
    {
        this._actions.Add(new UIActionConfig(action));
    }
}