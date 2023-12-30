public class CombinationAlgorithm : IAlgorithm
{
    public AlgorithmType Type { get; } = AlgorithmType.Combination;

    public virtual Dictionary<int, List<int>> Generate(int maxNumber, int combinationLength)
    {
       var returnValue = new Dictionary<int, List<int>>();

        if (maxNumber <= 0 && combinationLength <= 0)
        {
            return returnValue;
        }

        var numbers1 = new List<List<int>>();

        int[] numbers = new int[combinationLength];

        for (int i = 1; i <= maxNumber - combinationLength + 1; i++)
        {
            numbers[0] = i;
            this.GenerateNextNumber(numbers1, numbers, 1, combinationLength, maxNumber);
        }

        int j = 0;
        foreach (var number in numbers1)
        {
            returnValue.Add(j, number);
            j++;
        }

        return returnValue;
    }

    void GenerateNextNumber(List<List<int>> returnValue, int[] numbers, int index, int combinationLength, int maxNumber)
    {
        if (index >= combinationLength)
        {
            returnValue.Add(numbers.ToList());
            return;
        }

        for (int j = numbers[index - 1] + 1; j <= maxNumber - combinationLength + index + 1; j++)
        {
            numbers[index] = j;
            GenerateNextNumber(returnValue, numbers, index + 1, combinationLength, maxNumber);
        }
    }
}