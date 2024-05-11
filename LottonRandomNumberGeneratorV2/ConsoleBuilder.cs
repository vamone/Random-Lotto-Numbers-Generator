public class ConsoleBuilder
{
    public List<Action> _list { get; set; } = new List<Action>();

    public void SetUI(Action value)
    {
        this._list.Add(value);
    }

    public void BuildUI()
    {
        foreach (var item in this._list)
        {
            item?.Invoke();
        }
    }
}