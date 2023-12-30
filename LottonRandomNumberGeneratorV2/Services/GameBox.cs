public class GameBox
{
    readonly IEnumerable<IGameConfig> _games;

    readonly IEnumerable<IAlgorithm> _algorithms;

    public GameBox(IEnumerable<IGameConfig> games, IEnumerable<IAlgorithm> algorithms)
    {
        this._games = games;
        this._algorithms = algorithms;
    }

    public IEnumerable<IGameConfig> GetGames()
    {
        return this._games;
    }

    public IEnumerable<IAlgorithm> GetAlgorithms()
    {
        return this._algorithms.OrderBy(_ => (int)_.Type);
    }

    public IGameConfig GetGameById(int id)
    {
        return this._games.SingleOrDefault(x => (int)x.Type == id);
    }

    public IAlgorithm GetAlgorithmById(int id)
    {
        return this._algorithms.SingleOrDefault(x => (int)x.Type == id);
    }

    public IEnumerable<string> GenerateNumbers(IGameConfig gameConfig, IAlgorithm algorithm, int numberOfGames)
    {
        var returnResult = new List<string>();

        int setsCount = gameConfig.Sets.Count();

        if (algorithm.Type == AlgorithmType.Random || algorithm.Type == AlgorithmType.Index)
        {
            for (int i = 0; i < numberOfGames; i++)
            {
                var numbers = new Dictionary<int, List<int>>();

                for (int j = 0; j < setsCount; j++)
                {
                    var setNumbers = algorithm.Generate(gameConfig.Sets[j].MaxValueNumber, gameConfig.Sets[j].CombinationLength).SelectMany(x => x.Value).ToList();
                    numbers.Add(j, setNumbers);
                }

                returnResult.Add(this.FormatNumbers(numbers));
            }
        }

        if (algorithm.Type == AlgorithmType.Combination)
        {
            var numbers = new Dictionary<int, List<int>>();

            for (int i = 0; i < setsCount; i++)
            {
                var combinations = algorithm.Generate(gameConfig.Sets[i].MaxValueNumber, gameConfig.Sets[i].CombinationLength);

                int combinationsCount = combinations.Count();
                int chunkSize = combinationsCount / numberOfGames;

                var chunked = combinations.Chunk(chunkSize).SelectMany(x => x.OrderBy(_ => Guid.NewGuid())).Take(numberOfGames).ToList();
                for (int j = i; j < chunked.Count(); j++)
                {
                    numbers.Add(i > 0 ? (j * setsCount) - 1 : j * setsCount, chunked[j].Value);
                    j++;
                }
            }

            var chunkedNumbers = numbers.OrderBy(_ => _.Key).Chunk(setsCount).ToList();
            for (int i = 0; i < chunkedNumbers.Count(); i++)
            {
                var dic = chunkedNumbers[i].ToDictionary(x => x.Key, x => x.Value);
                returnResult.Add(this.FormatNumbers(dic));
            }
        }

        return returnResult;
    }

    public string FormatNumbers(Dictionary<int, List<int>> numbers)
    {
        var subList = new List<string>();

        var numbersToJoin = numbers.Select(x => x.Value);
        foreach (var items in numbersToJoin)
        {
            subList.Add(string.Join("-", items.OrderBy(_ => _)));
        }

        return string.Join(" | ", subList);
    }
}