using System.Collections.Generic;

public interface IAlgorithm
{
    AlgorithmType Type { get; }

    Dictionary<int, List<int>> Generate(int maxNumber, int combinationLength);
}