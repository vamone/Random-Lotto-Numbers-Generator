using System.Security.Cryptography;

public class RandomAlgorithm : IAlgorithm
{
    public AlgorithmType AlgorithmType { get; } = AlgorithmType.Random;

    public virtual List<List<int>> Generate(int maxNumber, int combinationLength)
    {
        var result = new List<List<int>>();

        if (maxNumber <= 0 && combinationLength <= 0)
        {
            return result;
        }

        var randomList = new List<int>();

        while (true)
        {
            int number = this.GetRandomInt(1, maxNumber);

            bool isExists = randomList.Contains(number);
            if (!isExists)
            {
                randomList.Add(number);
            }

            int numbersCount = randomList.Count;
            if (numbersCount == combinationLength)
            {
                break;
            }
        }

        result.Add(randomList);

        return result;
    }

    int GetRandomInt(int min, int max)
    {
        uint scale = uint.MaxValue;
        while (scale == uint.MaxValue)
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[256 * sizeof(int)]; // 4 bytes
            random.GetNonZeroBytes(bytes);

            scale = BitConverter.ToUInt32(bytes, 0);
        }

        return (int)(min + (max - min) *
            (scale / (double)uint.MaxValue));
    }
}