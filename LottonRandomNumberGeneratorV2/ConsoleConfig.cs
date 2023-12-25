// See https://aka.ms/new-console-template for more information

using LottonRandomNumberGeneratorV2;
using System.Threading.Tasks.Dataflow;

public class ConsoleConfig
{
    internal SetGamesConfig _setGamesConfig;

    public ConsoleConfig(Action<ConsoleConfig> config)
    {
        config.Invoke(this);
    }

    public WriteLineConfig WriteLineBeforeConfig { get; set; }

    public WriteLineConfig WriteLineAfterConfig { get; set; }

    public Func<string, bool> FuncValidateReadlineValue { get; set; }

    public string ValidationErrorMessage { get; set; }

    public bool IsRepeatQuestionIfValidationFailed { get; set; }

    public bool IsIgnoredWhenNull { get; set; }

    public Action Action { get; set; }

    public void SetGames(Action<SetGamesConfig> setGamesConfig)
    {
        this._setGamesConfig = new SetGamesConfig(setGamesConfig);
    }
}

public class SetGamesConfig
{
    internal List<GameConfig> _gameConfigs = new List<GameConfig>();

    public SetGamesConfig(Action<SetGamesConfig> config)
    {
        config.Invoke(this);
    }

    public void Add(Action<GameConfig> gameConfig)
    {
        this._gameConfigs.Add(new GameConfig(gameConfig));
    }
}

public class GameConfig
{
    public GameConfig(Action<GameConfig> config)
    {
        config.Invoke(this);
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public GameInput GameInput { get; set; }

    public bool IsGameInputSet { get; set; }

    public ConsoleConfig ConsoleConfig { get; set; }    

    public Action Action { get; set; }    

    public void SetGameInput(int mainMaxNumber, int mainCombinatioLength, int bonusMaxNumber, int bonusCombinatioLength)
    {
        this.GameInput = new GameInput();

        this.GameInput.GameName = this.Name;

        this.GameInput.MainMaxNumber = mainMaxNumber;
        this.GameInput.MainCombinatioLength = mainCombinatioLength;

        this.GameInput.BonusMaxNumber = bonusMaxNumber;
        this.GameInput.BonusCombinatioLength = bonusCombinatioLength;

        this.IsGameInputSet = true;
    }

    public void AddReadLine(Action<ConsoleConfig> consoleConfig)
    {
        this.ConsoleConfig = new ConsoleConfig(consoleConfig);
    }
}