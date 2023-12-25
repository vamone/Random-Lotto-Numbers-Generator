public class CombinationAlgorithm : IAlgorithm
{
    public ICollection<ICollection<int>> Generate(int maxNumber, int combinationLength)
    {
        var returnValue = new List<ICollection<int>>();

        int[] numbers = new int[combinationLength];

        for (int i = 1; i <= maxNumber - combinationLength + 1; i++)
        {
            numbers[0] = i;
            GenerateNextNumber(returnValue, numbers, 1, combinationLength, maxNumber);
        }

        return returnValue;
    }

    void GenerateNextNumber(ICollection<ICollection<int>> returnValue, int[] numbers, int index, int combinationLength, int maxNumber)
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