using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

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
        return this._algorithms;
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
        return new List<string> { "1-2-3-4-5-6 | 1-2", "1-2-3-4-5-6" };
    }
}