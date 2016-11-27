using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LottoRandomNumberGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter number of numbers:");
            var numberOfNumbers = Console.ReadLine();

            Console.WriteLine("Enter picked numbers:");
            var pickedNumbers = Console.ReadLine();

            Console.WriteLine("Enter number of lucky numbers:");
            var luckyNumberOfNumbers = Console.ReadLine();

            Console.WriteLine("Enter picked lucky numbers:");
            var pickedLuckyNumbers = Console.ReadLine();

            Console.WriteLine("---");

            int intNumberOrNumbers = Convert.ToInt32(numberOfNumbers);
            int intPickedNumbers = Convert.ToInt32(pickedNumbers);
            int intLuckyNumberOfNumbers = Convert.ToInt32(luckyNumberOfNumbers);
            int intPickedLuckyNumbers = Convert.ToInt32(pickedLuckyNumbers);

            var randomList = GetRandomNumbers(intNumberOrNumbers, intPickedNumbers);
            var randomLuckyList = GetRandomNumbers(intLuckyNumberOfNumbers, intPickedLuckyNumbers);

            string mainNumbers = string.Join("-", randomList);
            string luckyNumbers = string.Join("-", randomLuckyList);

            Console.WriteLine(mainNumbers);
            Console.WriteLine(luckyNumbers);

            Console.ReadLine();
        }

        static ICollection<int> GetRandomNumbers(int totalNumbers, int pickedNumbers)
        {
            var randomList = new List<int>();

            while (true)
            {
                int number = GetRandomInt(1, totalNumbers);

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

        static int GetRandomInt(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                var randomNumberProvider = new RNGCryptoServiceProvider();

                byte[] bytes = new byte[8];

                //byte[] randomBytes = new byte[256 * sizeof(int)];

                randomNumberProvider.GetBytes(bytes);

                scale = BitConverter.ToUInt32(bytes, 0);
            }

            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }
    }
}