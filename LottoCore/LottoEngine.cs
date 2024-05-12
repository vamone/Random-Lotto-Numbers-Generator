public interface ILottoEngine
{

}

public class LottoEngine : ILottoEngine
{
    readonly IEnumerable<IGameConfig> _games;

    readonly IEnumerable<IAlgorithm> _algorithms;

    public LottoEngine(IEnumerable<IGameConfig> games, IEnumerable<IAlgorithm> algorithms)
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

    public IGameConfig GetGameByType(GameType type)
    {
        return this._games.SingleOrDefault(x => x.Type == type);
    }

    public IAlgorithm GetAlgorithmById(int id)
    {
        return this._algorithms.SingleOrDefault(x => (int)x.Type == id);
    }

    public IAlgorithm GetAlgorithmByType(AlgorithmType type)
    {
        return this._algorithms.SingleOrDefault(x => x.Type == type);
    }

    public IEnumerable<string> GenerateNumbers(IGameConfig gameConfig, IAlgorithm algorithm, int numberOfGames)
    {
        var returnResult = new List<string>();

        int setsCount = gameConfig.Sets.Count();

        if (algorithm.Type == AlgorithmType.Random || algorithm.Type == AlgorithmType.Index)
        {
            for (int i = 0; i < numberOfGames; i++)
            {
                var numbers = new List<List<List<int>>>();

                foreach (var configSet in gameConfig.Sets)
                {
                    var setNumbers = algorithm.Generate(configSet.MaxValueNumber, configSet.CombinationLength);
                    numbers.Add(setNumbers);
                }

                returnResult.Add(this.FormatNumbers(numbers));
            }
        }

        if (algorithm.Type == AlgorithmType.Combination)
        {
            var dictionaryNumbers = new Dictionary<int, List<List<int>>>();

            int i = 0;
            foreach (var configSet in gameConfig.Sets)
            {
                var combinations = algorithm.Generate(configSet.MaxValueNumber, configSet.CombinationLength);
                int combinationsCount = combinations.Count();

                if (combinationsCount < numberOfGames)
                {
                    int intt = numberOfGames / combinationsCount;
                    for (int k = 0; k < intt; k++)
                    {
                        var nextCombinations = algorithm.Generate(configSet.MaxValueNumber, configSet.CombinationLength);
                        combinations.AddRange(nextCombinations);
                    }
                }

                int nextCombinationsCount = combinations.Count();
                int chunkSize = nextCombinationsCount / numberOfGames;

                int j = i;
                var chunked = combinations.Chunk(chunkSize)
                    .OrderBy(_ => Guid.NewGuid())
                    .Take(numberOfGames)
                    .Select(x => x.OrderBy(_ => Guid.NewGuid()).Take(1).ToList())
                    .ToList();

                foreach (var chunk in chunked)
                {
                    dictionaryNumbers.Add(i > 0 ? (j * setsCount) - 1 : j * setsCount, chunk);
                    j++;
                }

                i++;
            }

            var chunkedNumbers = dictionaryNumbers.OrderBy(_ => _.Key).Chunk(setsCount).ToList();
            foreach (var chunk in chunkedNumbers)
            {
                returnResult.Add(this.FormatNumbers(chunk.Select(x => x.Value).ToList()));
            }
        }

        return returnResult;
    }

    public string FormatNumbers(List<List<List<int>>> numbers)
    {
        var subList = new List<string>();

        foreach (var items in numbers)
        {
            foreach (var item in items)
            {
                subList.Add(string.Join("-", item.OrderBy(_ => _)));
            }
        }

        return string.Join(" | ", subList);
    }
}