using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LottoRandomNumberGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Lotto random number generator");
            Console.WriteLine("---");

            Console.WriteLine("What is the hightest number?");
            string numberOfNumbers = Console.ReadLine();
            if(!IsDigitsOnly(numberOfNumbers))
            {

            }

            Console.WriteLine("How many numbers?");
            string pickedNumbers = Console.ReadLine();

            Console.WriteLine("What is the hightest lucky number?");
            string luckyNumberOfNumbers = Console.ReadLine();

            string pickedLuckyNumbers = "0";

            if (luckyNumberOfNumbers != "0")
            {
                Console.WriteLine("How many lucky numbers?");
                pickedLuckyNumbers = Console.ReadLine();
            }   

            PrintNumbers(numberOfNumbers, pickedNumbers, luckyNumberOfNumbers, pickedLuckyNumbers);
        }

        static bool IsDigitsOnly(string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        static void PrintNumbers(string numberOfNumbers, string pickedNumbers, string luckyNumberOfNumbers, string pickedLuckyNumbers)
        {
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

            var answer = Console.ReadKey();
            if (answer.Key == ConsoleKey.Enter)
            {
                PrintNumbers(numberOfNumbers, pickedNumbers, luckyNumberOfNumbers, pickedLuckyNumbers);
            }
        }

        static ICollection<int> GetRandomNumbers(int totalNumbers, int pickedNumbers)
        {
            if(totalNumbers <= 0 && pickedNumbers <= 0)
            {
                return new List<int>();
            }
            
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

                //byte[] bytes = new byte[7];

                byte[] bytes = new byte[256 * sizeof(int)];

                randomNumberProvider.GetBytes(bytes);

                scale = BitConverter.ToUInt32(bytes, 0);
            }

            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }
    }
}