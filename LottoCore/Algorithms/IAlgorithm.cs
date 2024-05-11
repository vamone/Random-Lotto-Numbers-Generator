public interface IAlgorithm
{
    AlgorithmType Type { get; }

    List<List<int>> Generate(int maxNumber, int combinationLength);
}