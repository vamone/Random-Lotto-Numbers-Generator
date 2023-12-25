public class UIActionConfig
{
    public UIActionConfig(Action<UIActionConfig> config)
    {
        config.Invoke(this);
    }

    public Action Action { get; set; }
}