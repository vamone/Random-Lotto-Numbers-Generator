using LottonRandomNumberGeneratorV2.Algorithms;

namespace LottonRandomNumberGeneratorV2
{
    public static class Constants
    {
        public static Dictionary<int, IAlgorithm> GetAlgorithms()
        {
            var dictionary = new Dictionary<int, IAlgorithm>
            {
                { (int)AlgorithmType.Combination, new CombinationAlgorithm() },
                { (int)AlgorithmType.Random, new RandomAlgorithm() },
                { (int)AlgorithmType.Index, new IndexAlgorithm(new CombinationAlgorithm(), new RandomAlgorithm()) }
            };

            return dictionary;
        }

        public static Dictionary<int, IGameConfig> GetGameConfigs()
        {
            var dictionary = new Dictionary<int, IGameConfig>
            {
                { (int)GameType.Euromilions, 
                    new GameConfig(GameType.Euromilions, maxGamesCount: 70)
                    .ConfigSetOfNumbers(Enums.GameSetType.Main, 50, 5)
                    .ConfigSetOfNumbers(Enums.GameSetType.LuckyStars, 12, 2)
                    .SetUrl("https://www.national-lottery.co.uk/games/euromillions") },

                { (int)GameType.Setforlive, 
                    new GameConfig(GameType.Setforlive, maxGamesCount: 70)
                    .ConfigSetOfNumbers(Enums.GameSetType.Main, 47, 5)
                    .ConfigSetOfNumbers(Enums.GameSetType.LifeBall, 10, 1)
                    .SetUrl("https://www.national-lottery.co.uk/games/set-for-life") },

                { (int)GameType.Lotto, 
                    new GameConfig(GameType.Lotto, maxGamesCount: 70)
                    .ConfigSetOfNumbers(Enums.GameSetType.Main, 59, 6)
                    .SetUrl("https://www.national-lottery.co.uk/games/lotto") },

                { (int)GameType.Thunderball, 
                    new GameConfig(GameType.Thunderball, maxGamesCount: 70)
                    .ConfigSetOfNumbers(Enums.GameSetType.Main, 39, 5)
                    .ConfigSetOfNumbers(Enums.GameSetType.Thunderball, 14, 1)
                    .SetUrl("https://www.national-lottery.co.uk/games/thunderball") }
            };

            return dictionary;
        }
    }
}