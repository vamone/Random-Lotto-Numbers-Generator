public class Generator
{
    readonly ConsoleDecorator _consoleDecorator;

    readonly IAlgorithm _combinationAlgorithm;

    readonly IAlgorithm _randomAlgorithm;

    public Generator(ConsoleDecorator consoleDecorator, IAlgorithm _combinationAlgorithm, IAlgorithm randomAlgorithm)
    {
        this._consoleDecorator = consoleDecorator;
        this._combinationAlgorithm = _combinationAlgorithm;
        this._randomAlgorithm = randomAlgorithm;
    }

    public void PrintNumbers(Game gameInput)
    {
        this._consoleDecorator.WriteLine($"Generating for {gameInput.Name} using {gameInput.Algorithm} algorithm...", WriteLineSeparator.Both);

        try
        {
            List<List<int>> randomList = new List<List<int>>();
            List<List<int>> randomLuckyList = new List<List<int>>();

            var algorithm = this.ResolveAlgorithm(gameInput.Algorithm);

            randomList = algorithm.Generate(gameInput.MainMaxNumber, gameInput.MainCombinatioLength, gameInput.Take);

            if (gameInput.BonusMaxNumber > 0 && gameInput.BonusCombinatioLength > 0)
            {
                randomLuckyList = algorithm.Generate(gameInput.BonusMaxNumber, gameInput.BonusCombinatioLength, gameInput.Take);
            }

            int i = 0;
            foreach (var random in randomList)
            {
                string mainNumbers = string.Join("-", random.OrderBy(_ => _));
                this._consoleDecorator.Write(mainNumbers);

                if (randomLuckyList.Any())
                {
                    this._consoleDecorator.Write(" | ");

                    string luckyNumbers = string.Join("-", randomLuckyList[i].OrderBy(_ => _));
                    this._consoleDecorator.WriteLine(luckyNumbers);
                }

                if (randomList.Count > 1 && randomList.Count != (i + 1))
                {
                    this._consoleDecorator.WriteLine("---");
                }

                i++;
            }
        }
        catch (Exception ex)
        {
            this._consoleDecorator.WriteLine(ex.Message, WriteLineSeparator.Before);
            this._consoleDecorator.WriteLine(ex.ToString(), WriteLineSeparator.After);
        }
    }

    internal IAlgorithm ResolveAlgorithm(AlgorithmType type)
    {
        if (type == AlgorithmType.Random)
        {
            return this._randomAlgorithm;
        }

        if (type == AlgorithmType.Combination)
        {
            return this._combinationAlgorithm;
        }

        throw new ArgumentOutOfRangeException(nameof(type));
    }
}