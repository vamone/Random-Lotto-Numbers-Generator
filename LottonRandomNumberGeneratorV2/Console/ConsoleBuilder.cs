public class ConsoleBuilder
{
    public List<UIConfig> _list { get; set; } = new List<UIConfig>();

    public void SetUI(Action<UIConfig> value)
    {
        this._list.Add(new UIConfig(value));
    }

    public void Run()
    {
        var actions = this._list.SelectMany(x => x._actions);
        foreach (var item in actions)
        {
            item.Action?.Invoke();
        }
    }
}