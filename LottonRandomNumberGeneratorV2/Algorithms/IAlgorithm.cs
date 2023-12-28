public interface IAlgorithm
{
    AlgorithmType AlgorithmType { get; }

    List<List<int>> Generate(int maxNumber, int combinationLength);
}