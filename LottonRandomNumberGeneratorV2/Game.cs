public class Game
{
    public Game(GameType type, int mainMaxNumber, int mainCombinatioLength, int bonusMaxNumber, int bonusCombinatioLength, AlgorithmType algorithmType = AlgorithmType.Combination)
    {
        this.Type = type;
        this.Name = type.ToString();

        this.MainMaxNumber = mainMaxNumber;
        this.MainCombinatioLength = mainCombinatioLength;

        this.BonusMaxNumber = bonusMaxNumber;
        this.BonusCombinatioLength = bonusCombinatioLength;

        this.Algorithm = algorithmType;
    }

    public GameType Type { get; private set; }

    public string Name { get; set; }

    public int MainMaxNumber { get; set; }

    public int MainCombinatioLength { get; set; }

    public int BonusMaxNumber { get; set; }

    public int BonusCombinatioLength { get; set; }

    public int Take { get; set; } = 1;

    public AlgorithmType Algorithm { get; set; } = AlgorithmType.Combination;
}