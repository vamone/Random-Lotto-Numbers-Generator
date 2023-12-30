public interface IGameConfig
{
    GameType Type { get; }

    string Name { get; }

    List<SetConfig> Sets { get; }

    IGameConfig ConfigSetOfNumbers(int maxNumber, int combinatiocLength);
}

public class GameConfig : IGameConfig
{
    public GameConfig(GameType type)
    {
        this.Type = type;
        this.Name = type.ToString();
    }

    public GameType Type { get; private set; } = GameType.None;

    public string Name { get; private set; }

    public List<SetConfig> Sets { get; private set; } = new List<SetConfig>();

    public IGameConfig ConfigSetOfNumbers(int maxNumber, int combinationLength)
    {
        this.Sets.Add(new SetConfig(maxNumber, combinationLength));
        return this;
    }
}

public class SetConfig
{
    public SetConfig(int maxValueNumber, int combinationLength)
    {
        this.MaxValueNumber = maxValueNumber;
        this.CombinationLength = combinationLength;
    }

    public int MaxValueNumber { get; private set; }

    public int CombinationLength { get; private set; }
}