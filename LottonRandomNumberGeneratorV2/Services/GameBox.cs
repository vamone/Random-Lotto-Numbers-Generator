using System.Text.RegularExpressions;

public class GameBox
{
    readonly List<Game> _games;

    public GameBox(List<Game> games)
    {
        this._games = games;
    }

    public List<Game> GetGames()
    {
        return this._games;
    }

    public bool IsGameOption(int option)
    {
        return this._games.Any(x => (int)x.Type == option);
    }

    public Game ResolveGame(string command)
    {
        Game customGame = null;

        var matches = Regex.Matches(command.Trim(), @"(-([A-z])\s?([0-9]))").ToList();
        foreach (var match in matches)
        {
            string actionValue = match.Groups[2].Value;
            string numberValue = match.Groups[3].Value;

            if (actionValue == "g")
            {
                customGame = this._games.SingleOrDefault(x => (int)x.Type == numberValue.ToInt32());
            }

            if (customGame != null && actionValue == "r")
            {
                customGame.Take = numberValue.ToInt32();
            }

            if (customGame != null && actionValue == "a")
            {
                if(numberValue.ToInt32() == (int)AlgorithmType.Random)
                {
                    customGame.Algorithm = AlgorithmType.Random;
                }

                if (numberValue.ToInt32() == (int)AlgorithmType.Combination)
                {
                    customGame.Algorithm = AlgorithmType.Combination;
                }

                if (numberValue.ToInt32() == (int)AlgorithmType.Index)
                {
                    customGame.Algorithm = AlgorithmType.Index;
                }
            }

            if (customGame != null && actionValue == "c")
            {
                customGame.ChunkInto = numberValue.ToInt32();
            }
        }

        return customGame;
    }

    public Game ResolveGame(int gameId)
    {
        return this.ResolveGame($"-g{gameId}");
    }
}