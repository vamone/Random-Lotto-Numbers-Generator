using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameManager
{
    readonly IEnumerable<IAlgorithm> _algorithms;

    public GameManager(IEnumerable<IAlgorithm> algorithms)
    {
        this._algorithms = algorithms;
    }

    public List<string> GenerateNumbers(GameConfig gameConfig)
    {
        var returnResult = new List<string>();

        return returnResult;

       
        //var algorithm = this._algorithms.SingleOrDefault(x => x.AlgorithmType == game.Algorithm);
        //if (algorithm == null)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(algorithm));
        //}

        //if (algorithm.AlgorithmType == AlgorithmType.Random)
        //{
        //    for (int i = 0; i < game.Take; i++)
        //    {
        //        var mainNumbers = algorithm.Generate(game.MainMaxNumber, game.MainCombinatioLength);
        //        var bonusNumbers = algorithm.Generate(game.BonusMaxNumber, game.BonusCombinatioLength);

        //        returnResult.Add(this.FormatNumbersIntoString(mainNumbers, bonusNumbers));
        //    }
        //}

        //if (algorithm.AlgorithmType == AlgorithmType.Combination)
        //{
        //    var mainNumbers = algorithm.Generate(game.MainMaxNumber, game.MainCombinatioLength);
        //    var bonusNumbers = algorithm.Generate(game.BonusMaxNumber, game.BonusCombinatioLength);

        //    if (game.ChunkInto <= 0)
        //    {
        //        var filteredMainNumbers = mainNumbers.OrderBy(_ => Guid.NewGuid()).Take(game.Take).ToList();
        //        var filteredBonusNumbers = bonusNumbers.OrderBy(_ => Guid.NewGuid()).Take(game.Take).ToList();

        //        for (int i = 0; i < game.Take; i++)
        //        {
        //            returnResult.Add(this.FormatNumbersIntoString(new List<List<int>> { filteredMainNumbers[i] }, new List<List<int>> { filteredBonusNumbers[i] }));
        //        }
        //    }

        //    if (game.ChunkInto >= 1)
        //    {
        //        int mainTotalCount = mainNumbers.Count();
        //        int bonusTotalCount = bonusNumbers.Count();

        //        int mainSize = mainTotalCount / game.ChunkInto;
        //        int bonusSize = bonusTotalCount / game.ChunkInto;

        //        var chunkedMainNumbers = mainTotalCount > 0 ? mainNumbers.Chunk(mainSize) : null;
        //        var chunkedBonusNumbers = bonusTotalCount > 0 ? bonusNumbers.Chunk(bonusSize) : null;

        //        var filteredMainNumbers = chunkedMainNumbers?.SelectMany(x => x.OrderBy(_ => Guid.NewGuid()).Take(game.Take).ToList()).ToList();
        //        var filteredBonusNumbers = chunkedBonusNumbers?.SelectMany(x => x.OrderBy(_ => Guid.NewGuid()).Take(game.Take).ToList()).ToList();

        //        for (int i = 0; i < game.ChunkInto; i++)
        //        {
        //            returnResult.Add(this.FormatNumbersIntoString(new List<List<int>> { filteredMainNumbers[i] }, new List<List<int>> { filteredBonusNumbers[i] }));
        //        }
        //    }
        //}

        //if (algorithm.AlgorithmType == AlgorithmType.Index)
        //{
        //    for (int i = 0; i < game.Take; i++)
        //    {
        //        var mainNumbers = algorithm.Generate(game.MainMaxNumber, game.MainCombinatioLength);
        //        var bonusNumbers = algorithm.Generate(game.BonusMaxNumber, game.BonusCombinatioLength);

        //        returnResult.Add(this.FormatNumbersIntoString(mainNumbers, bonusNumbers));
        //    }
        //}

        return returnResult;
    }

    public string FormatNumbersIntoString(List<List<int>> mainNumbers, List<List<int>> bonusNumbers)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < mainNumbers.Count(); i++)
        {
            string main = string.Join("-", mainNumbers[i].OrderBy(_ => _));
            sb.Append(main);

            if (bonusNumbers != null && bonusNumbers.Any())
            {
                if (bonusNumbers[i].Any())
                {
                    sb.Append(" | ");

                    string bonus = string.Join("-", bonusNumbers[i].OrderBy(_ => _));
                    sb.Append(bonus);
                }
            }
        }

        return sb.ToString();
    }
}
