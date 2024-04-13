using LottonRandomNumberGeneratorV2.Enums;

public interface IGameConfig
{
    GameType Type { get; }

    string Name { get; }

    List<ISetConfig> Sets { get; }

    IGameConfig ConfigSetOfNumbers(GameSetType gameSetType, int maxNumber, int combinatiocLength);

    string Url { get; }

    IGameConfig SetUrl(string url);
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

    public List<ISetConfig> Sets { get; private set; } = new List<ISetConfig>();

    public IGameConfig ConfigSetOfNumbers(GameSetType gameSetType, int maxNumber, int combinationLength)
    {
        this.Sets.Add(new SetConfig(gameSetType, maxNumber, combinationLength));
        return this;
    }

    public IGameConfig SetUrl(string url)
    {
        this.Url = url;
        return this;
    }

    public string Url { get; private set; }
}

public interface ISetConfig
{
    GameSetType GameSetType { get; }

    int MaxValueNumber { get; }

   int CombinationLength { get; }
}

public class SetConfig : ISetConfig
{
    public SetConfig(GameSetType gameSetType,int maxValueNumber, int combinationLength)
    {
        this.GameSetType = gameSetType;
        this.MaxValueNumber = maxValueNumber;
        this.CombinationLength = combinationLength;
    }

    public GameSetType GameSetType { get; private set; }

    public int MaxValueNumber { get; private set; }

    public int CombinationLength { get; private set; }
}