// See https://aka.ms/new-console-template for more information

using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Extensions;
using System.Text.RegularExpressions;

public class ConsoleBuilder
{
    public List<ConsoleConfig> _list { get; set; } = new List<ConsoleConfig>();

    public void Add(Action<ConsoleConfig> value)
    {
        this._list.Add(new ConsoleConfig(value));
    }

    public GameInput GetGameInput()
    {
        foreach (var item in this._list)
        {
            string value = this.GetReadLineValue(item);

            var option = item._setGamesConfig._gameConfigs.SingleOrDefault(x => x.Id == value.ToInt32());
            if (option != null)
            {
                if(option.Action != null)
                {
                    option.Action.Invoke();
                }

                if (option.IsGameInputSet)
                {
                    return option.GameInput;
                }

                GameInput gameInput = null;

                string readValue = this.GetReadLineValue(option.ConsoleConfig);

                var matches = Regex.Matches(readValue.Trim(), @"(-([A-z])\s?([0-9]))").ToList();
                foreach (var match in matches)
                {
                    string actionValue = match.Groups[2].Value;
                    string numberValue = match.Groups[3].Value;

                    if (actionValue == "g")
                    {
                        gameInput = item._setGamesConfig._gameConfigs.SingleOrDefault(x => x.Id == numberValue.ToInt32())?.GameInput;
                    }

                    if (gameInput != null && actionValue == "t")
                    {
                        gameInput.Take = numberValue.ToInt32();
                    }

                    if (gameInput != null && actionValue == "a")
                    {
                        gameInput.Algorithm = numberValue.ToInt32() == 1 ? AlgorithmType.Random : AlgorithmType.Combination;
                    }

                }

                return gameInput;

            }
        }

        return null;
    }

    public Action ResolveValues(string value)
    {
        throw new NotImplementedException();
    }

    internal string GetReadLineValue(ConsoleConfig item)
    {
        if (item.WriteLineBeforeConfig != null && !string.IsNullOrWhiteSpace(item.WriteLineBeforeConfig.Value))
        {
            if (item.WriteLineBeforeConfig.LineSpacers == WriteLineSpacers.Before || item.WriteLineBeforeConfig.LineSpacers == WriteLineSpacers.Both)
            {
                Console.WriteLine("---");
            }

            if (item.WriteLineBeforeConfig.IsWriteLine)
            {
                Console.WriteLine(item.WriteLineBeforeConfig.Value);
            }
            else
            {
                Console.Write(item.WriteLineBeforeConfig.Value);
            }

            if (item.WriteLineBeforeConfig.LineSpacers == WriteLineSpacers.After || item.WriteLineBeforeConfig.LineSpacers == WriteLineSpacers.Both)
            {
                Console.WriteLine("---");
            }
        }

        if (item?._setGamesConfig?._gameConfigs?.Count > 0)
        {
            int i = 0;

            foreach (var gameConfig in item?._setGamesConfig?._gameConfigs)
            {
                i++;
                Console.WriteLine($"{i}. {gameConfig.Name}");
            }
        }

        if (item.WriteLineAfterConfig != null && !string.IsNullOrWhiteSpace(item.WriteLineAfterConfig.Value))
        {
            if (item.WriteLineAfterConfig.LineSpacers == WriteLineSpacers.Before || item.WriteLineAfterConfig.LineSpacers == WriteLineSpacers.Both)
            {
                Console.WriteLine("---");
            }

            if (item.WriteLineAfterConfig.IsWriteLine)
            {
                Console.WriteLine(item.WriteLineAfterConfig.Value);
            }
            else
            {
                Console.Write(item.WriteLineAfterConfig.Value);
            }

            if (item.WriteLineAfterConfig.LineSpacers == WriteLineSpacers.After || item.WriteLineAfterConfig.LineSpacers == WriteLineSpacers.Both)
            {
                Console.WriteLine("---");
            }
        }

        string? readLineValue = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(readLineValue) && item.IsIgnoredWhenNull)
        {
            return "0";
        }

        bool isValidationSuccessful = !string.IsNullOrWhiteSpace(readLineValue) && item.FuncValidateReadlineValue.Invoke(readLineValue);
        if (!isValidationSuccessful)
        {
            Console.WriteLine(item.ValidationErrorMessage);

            if (item.IsRepeatQuestionIfValidationFailed)
            {
                return this.GetReadLineValue(item);
            }
        }

        return readLineValue;
    }

    public void PrintHelp()
    {
        Console.WriteLine("---");

        Console.WriteLine("-g 1 = game and game number");
        Console.WriteLine("-r 2 = print repetitions and it's number");
        Console.WriteLine("-a 1-2 = algorithm and random 1, combination 2");

        Console.WriteLine("---");
    }
}
