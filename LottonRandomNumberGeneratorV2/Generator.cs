// See https://aka.ms/new-console-template for more information

using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public class Generator
{
    readonly IAlgorithm _combinationAlgorithm;

    public Generator()
    {
        this._combinationAlgorithm = new CombinationAlgorithm();
    }

    public void PrintNumbers(Game gameInput)
    {
        Console.WriteLine("---");
        Console.WriteLine($"Generating for {gameInput.Name} using {gameInput.Algorithm} algorithm...");
        Console.WriteLine("---");

        try
        {
            ICollection<int> randomList = new List<int>();
            ICollection<int> randomLuckyList = new List<int>();

            if (gameInput.Algorithm == AlgorithmType.Random)
            {
                randomList = this.GetRandomNumbers(gameInput.MainMaxNumber, gameInput.MainCombinatioLength);

                if (gameInput.BonusMaxNumber > 0 && gameInput.BonusCombinatioLength > 0)
                {
                    randomLuckyList = this.GetRandomNumbers(gameInput.BonusMaxNumber, gameInput.BonusCombinatioLength);
                }
            }

            if (gameInput.Algorithm == AlgorithmType.Combination)
            {
                randomList = this._combinationAlgorithm.Generate(gameInput.MainMaxNumber, gameInput.MainCombinatioLength).OrderByDescending(_ => Guid.NewGuid()).Take(gameInput.Take).FirstOrDefault();

                if (gameInput.BonusMaxNumber > 0 && gameInput.BonusCombinatioLength > 0)
                {
                    randomLuckyList = this._combinationAlgorithm.Generate(gameInput.BonusMaxNumber, gameInput.BonusCombinatioLength).OrderByDescending(_ => Guid.NewGuid()).Take(gameInput.Take).FirstOrDefault();
                }
            }

            string mainNumbers = string.Join("-", randomList);
            Console.WriteLine(mainNumbers);

            if (randomLuckyList.Any())
            {
                string luckyNumbers = string.Join("-", randomLuckyList);
                Console.WriteLine(luckyNumbers);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.ToString());
        }

        //var answer = Console.ReadKey();
        //if (answer.Key == ConsoleKey.Enter)
        //{
        //    PrintNumbers(numberOfNumbers, pickedNumbers, luckyNumberOfNumbers, pickedLuckyNumbers);
        //}
    }

    ICollection<int> GetRandomNumbers(int totalNumbers, int pickedNumbers)
    {
        if (totalNumbers <= 0 && pickedNumbers <= 0)
        {
            return new List<int>();
        }

        var randomList = new List<int>();

        while (true)
        {
            int number = this.GetRandomInt(1, totalNumbers);

            bool isExists = randomList.Contains(number);
            if (!isExists)
            {
                randomList.Add(number);
            }

            int numbersCount = randomList.Count;
            if (numbersCount == pickedNumbers)
            {
                break;
            }
        }

        return randomList;
    }

    int GetRandomInt(int min, int max)
    {
        uint scale = uint.MaxValue;
        while (scale == uint.MaxValue)
        {
            //var randomNumberProvider = new RNGCryptoServiceProvider();

            var random = RandomNumberGenerator.Create();
            var bytes = new byte[256 * sizeof(int)]; // 4 bytes
            random.GetNonZeroBytes(bytes);

            //byte[] bytes = new byte[7];

            //byte[] bytes = new byte[256 * sizeof(int)];

            //randomNumberProvider.GetBytes(bytes);

            scale = BitConverter.ToUInt32(bytes, 0);
        }

        return (int)(min + (max - min) *
            (scale / (double)uint.MaxValue));
    }
}